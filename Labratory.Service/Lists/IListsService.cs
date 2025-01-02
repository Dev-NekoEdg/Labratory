using Labratory.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Service.Lists
{
    public interface IListsService
    {
        Task<IList<ListsDto>> GetLists();

        Task<ListsDto> GetList(int listId);

        Task<ListsDto> CreateList(ListsDto list);

        Task<ListsDto> UpdateList(ListsDto list);

        Task<bool> DeleteList(int listId);

        Task<FilterEnvelop<IList<ListsDto>>> GetFilteredLists(FilterEnvelop<FilterSearch> filter);

    }
}
