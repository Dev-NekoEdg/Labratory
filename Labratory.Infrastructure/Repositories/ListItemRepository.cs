using Labratory.Domain.Entities;
using Labratory.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Infrastructure.Repositories
{
    public class ListItemRepository : IListItemRepository
    {
        private readonly LaboratoryContext context;

        public ListItemRepository(LaboratoryContext context)
        {
            this.context = context;
        }

        public async Task<ListsItems> CreateAsync(ListsItems listsItem)
        {
            await this.context.ListsItems.AddAsync(listsItem);
            await this.context.SaveChangesAsync();
            await this.context.ListsItems.AddAsync(listsItem);
            return listsItem;
        }

        public async Task<bool> DeleteAsync(int listItemId)
        {
            var item = await this.context.ListsItems.SingleOrDefaultAsync(x => x.Id == listItemId);
            this.context.ListsItems.Remove(item);

            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<ListsItemsJoinSet>> GetAllAsync(int listId)
        {
            return await this.context.ListsItems
                .Join(this.context.Lists,
                     items => items.ListId,
                     list => list.Id,
                     (items, list) => new ListsItemsJoinSet { 
                         Id = items.Id,
                         ListId= items.ListId,
                         ListName = list.Name,
                         Name= items.Name,
                         ImageUrl = items.ImageUrl,
                         Description = items.Description,
                         Complete= items.Complete,
                     })
                .Where(x => x.ListId == listId).ToListAsync();
        }

        public async Task<ListsItemsJoinSet> GetAsync(int listItemId)
        {
            return await this.context.ListsItems
                .Join(this.context.Lists,
                      items => items.ListId,
                      list => list.Id,
                      (items, list) => new ListsItemsJoinSet {
                          Id = items.Id,
                          ListId = items.ListId,
                          ListName = list.Name,
                          Name = items.Name,
                          ImageUrl = items.ImageUrl,
                          Description = items.Description,
                          Complete = items.Complete,
                      })
                .SingleOrDefaultAsync(x => x.Id == listItemId);
        }

        public async Task<ListsItems> UpdateAsync(ListsItems listsItem)
        {
            this.context.Entry<ListsItems>(listsItem).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return listsItem;
        }
    }
}
