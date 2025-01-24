using Labratory.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Service.ListItems
{
    public interface IListItemService
    {
        Task<IList<ListsItemsDto>> GetListsItemsAsync(int listId);

        Task<ListsItemsDto> GetListsItemsAsync(int listId, int listItemId);

        Task<ListsItemsDto> CreateListsItemsAsync(ListsItemsDto newListItem);

        Task<ListsItemsDto> UpdateListsItemsAsync(ListsItemsDto newListItem);
        
        Task<bool> DeleteListsItemsAsync(ListsItemsDto newListItem);

        Task<string> UpdateListsItemImageAsync(int listId, int listItemId, string ext, Stream file);

        Task<bool> LoadListsItemAsync(int listId, string extention, Stream file);

        Task<FilterEnvelop<IList<ListsItemsDto>>> GetFilteredListsItemsAsync(int listId, FilterEnvelop<FilterSearch> filter);
    }
}
