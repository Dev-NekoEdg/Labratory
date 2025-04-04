using AutoMapper;
using Labratory.Domain.Dtos;
using Labratory.Domain.Entities;
using Labratory.Service.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Service.Lists
{
    public class ListsService : IListsService
    {
        private readonly IListsRepository listsRepository;
        private readonly IMapper mapper;

        public ListsService(
            IListsRepository listsRepository,
            IMapper mapper
            )
        {
            this.listsRepository = listsRepository;
            this.mapper = mapper;
        }

        public async Task<FilterEnvelop<IList<ListsDto>>> GetFilteredLists(FilterEnvelop<FilterSearch> filter)
        {
            var result = new FilterEnvelop<IList<ListsDto>>();
            var data = await this.listsRepository.GetFilteredLists(filter);
            var mappedData = mapper.Map<List<ListsDto>>(data.Item1);

            result.CurrentPage = filter.CurrentPage;
            result.PageSize = filter.PageSize;
            result.TotalRecords = data.Item2;
            decimal pages = (decimal)result.TotalRecords / (decimal)filter.PageSize;
            result.Pages = (int)Math.Ceiling(pages);
            result.Data = mappedData;

            return result;
        }

        public async Task<ListsDto> CreateList(ListsDto list)
        {
            var entity = this.mapper.Map<Domain.Entities.Lists>( list );
            var result  = await this.listsRepository.InsertList( entity );
            return this.mapper.Map<ListsDto>( result );
        }

        public async Task<bool> DeleteList(int listId)
        {
            return await this.listsRepository.DeleteList(listId);
        }

        public async Task<ListsDto> GetList(int listId)
        {
            var result = await this.listsRepository.GetList(listId);
            return mapper.Map<ListsDto>(result);
        }

        public async Task<IList<ListsDto>> GetLists()
        {
            var result = await this.listsRepository.GetAllLists();
            return mapper.Map<List<ListsDto>>( result );
        }


        public async Task<ListsDto> UpdateList(ListsDto list)
        {
            var entity = this.mapper.Map<Domain.Entities.Lists>( list );
            await this.listsRepository.UpdateList( entity );

            return list;
        }

    }
}
