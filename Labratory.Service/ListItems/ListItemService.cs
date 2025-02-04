using AutoMapper;
using Labratory.Domain.Dtos;
using Labratory.Domain.Entities;
using Labratory.Domain.Exceptions;
using Labratory.Service.BlobStorage;
using Labratory.Service.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Service.ListItems
{
    public class ListItemService : IListItemService
    {
        private readonly IListItemRepository repository;
        private readonly IMapper mapper;
        private readonly IBlobStorageService blobStorageService;

        public ListItemService(IListItemRepository repository,
            IMapper mapper,
            IBlobStorageService blobStorageService)
        {

            this.repository = repository;
            this.mapper = mapper;
            this.blobStorageService = blobStorageService;
        }

        public async Task<ListsItemsDto> CreateListsItemsAsync(ListsItemsDto newListItem)
        {
            var itemMapped = this.mapper.Map<ListsItems>(newListItem);
            var item = await this.repository.CreateAsync(itemMapped);

            return this.mapper.Map<ListsItemsDto>(item);
        }

        public async Task<bool> DeleteListsItemsAsync(ListsItemsDto newListItem)
        {
            return await this.repository.DeleteAsync(newListItem.Id);
        }

        public async Task<IList<ListsItemsDto>> GetListsItemsAsync(int listId)
        {
            var result = await this.repository.GetAllAsync(listId);
            return this.mapper.Map<List<ListsItemsDto>>(result);
        }

        public async Task<ListsItemsDto> GetListsItemsAsync(int listId, int listItemId)
        {
            var item = await this.repository.GetAsync(listItemId);
            return this.mapper.Map<ListsItemsDto>(item);
        }

        public async Task<FilterEnvelop<IList<ListsItemsDto>>> GetFilteredListsItemsAsync(int listId, FilterEnvelop<FilterSearch> filter)
        {
            var result = new FilterEnvelop<IList<ListsItemsDto>>();
            (IList<ListsItemsJoinSet>, int) items = await this.repository.GetFilteredAsync(listId, filter);

            var data = this.mapper.Map<IList<ListsItemsDto>>(items.Item1);

            result.CurrentPage = filter.CurrentPage;
            result.PageSize = filter.PageSize;
            result.TotalRecords = items.Item2;
            decimal pages = (decimal)result.TotalRecords / (decimal)result.PageSize;
            result.Pages = (int)Math.Ceiling(pages);

            result.Data = data;
            return result;
        }

        public async Task<ListsItemsDto> UpdateListsItemsAsync(ListsItemsDto newListItem)
        {
            var itemMapped = this.mapper.Map<ListsItems>(newListItem);
            var item = await this.repository.UpdateAsync(itemMapped);

            return this.mapper.Map<ListsItemsDto>(item);
        }

        public async Task<string> UpdateListsItemImageAsync(int listId, int listItemId, string ext, Stream file)
        {
            var newName = Guid.NewGuid().ToString();
            var fullName = $"List-{listId}/{newName}";

            var item = await this.GetListsItemsAsync(listId, listItemId);
            if (item == null)
            {
                throw new NotFoundException($"List item {listItemId} was not found.");
            }
            string oldName = item.ImageUrl;

            var result = await this.blobStorageService.SaveImageIntoBlobStorage(ext, fullName, file);

            item.ImageUrl = result;

            await this.UpdateListsItemsAsync(item);

            if (!string.IsNullOrEmpty(oldName))
            {
            // TODO: Validar por que no borra el archivo viejo.
                await this.blobStorageService.DeleteImageIntoBlobStorage(oldName);
            }

            return item.ImageUrl;
        }

        public async Task<bool> LoadListsItemAsync(int listId, string ex, Stream file)
        {
            List<ListsItems> items = new List<ListsItems>();
            using (StreamReader sr = new StreamReader(file))
            {
                sr.ReadLine();
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();

                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] lineDivided = line.Split(';');
                        items.Add(await this.ConvertStringToListsItems(listId, lineDivided));
                    }
                    //lineas.Add(sr.ReadLine());
                }
            }

            await this.repository.AddRangeItems(items);
            return true;
        }

        private async Task<ListsItems> ConvertStringToListsItems(int listId, string[] line)
        {
            return new ListsItems
            {
                ListId = listId,
                Name = line[0],
                Description = line[1],
                ImageUrl = line[2],
                Complete = !string.IsNullOrWhiteSpace(line[3])
            };
        }
    }
}
