using Labratory.Domain.Dtos;
using Labratory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Service.IRepositories
{
    public interface IListItemRepository
    {
        Task<IList<ListsItemsJoinSet>> GetAllAsync(int listId);

        Task<ListsItemsJoinSet> GetAsync(int listItemId);

        Task<ListsItems> CreateAsync(ListsItems listsItem);

        Task<ListsItems> UpdateAsync(ListsItems listsItem);

        Task<bool> DeleteAsync(int listItemId);
        Task AddRangeItems(List<ListsItems> items);

        Task<(IList<ListsItemsJoinSet>, int)> GetFilteredAsync(int listId, FilterEnvelop<FilterSearch> filter);
    }
}
