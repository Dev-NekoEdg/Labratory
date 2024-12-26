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

        public async Task<ListsItemsDto> UpdateListsItemsAsync(ListsItemsDto newListItem)
        {
            var itemMapped = this.mapper.Map<ListsItems>(newListItem);
            var item = await this.repository.UpdateAsync(itemMapped);

            return this.mapper.Map<ListsItemsDto>(item);
        }

        public async Task<bool> UpdateListsItemImageAsync(int listId, int listItemId, string ext, Stream file)
        {
            var name = $"List-{listId}/{listItemId}";

            var item = await this.GetListsItemsAsync(listId, listItemId);
            if (item == null)
            {
                throw new NotFoundException($"List item {listItemId} was not found.");
            }

            var result = await this.blobStorageService.SaveImageIntoBlobStorage(ext, name, file);

            item.ImageUrl = result;

            await this.UpdateListsItemsAsync(item);


            return true;
        }

        public async Task<bool> LoadListsItemAsync(int listId, string ex, Stream file)
        {
            List<ListsItemsDto> items = new List<ListsItemsDto>();
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
            var x = items;
            return true;
        }

        private async Task<ListsItemsDto> ConvertStringToListsItems(int listId, string[] line)
        {
            return new ListsItemsDto
            {
                ListId = listId,
                Name = line[1],
                Description = line[2],
                ImageUrl = line[3]
            };
        }
    }
}
