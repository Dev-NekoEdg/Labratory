using Labratory.Domain.Dtos;
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
    public class ListsRepository : IListsRepository
    {
        private readonly LaboratoryContext context;

        public ListsRepository(LaboratoryContext context)
        {
            this.context = context;
        }


        public async Task<bool> DeleteList(int id)
        {
            var item = await this.context.Lists.SingleAsync(x => x.Id == id);
            this.context.Lists.Remove(item);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<(IList<Lists>, int)> GetFilteredLists(FilterEnvelop<FilterSearch> filter)
        {
            var result = new FilterEnvelop<IList<Lists>>();
            var baseQuery = this.context.Lists.AsQueryable();

            if (filter.Data.Field.ToLower() == "nombre")
            {
                baseQuery = baseQuery.Where(b => b.Name.ToLower().Contains(filter.Data.Value.ToLower()));
            }
            if (filter.Data.Field.ToLower() == "id")
            {
                baseQuery = baseQuery.Where(b => b.Id == Convert.ToInt32(filter.Data.Value));
            }

            int countTotal = baseQuery.Count();
            baseQuery = baseQuery.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize);

            return (await baseQuery.ToListAsync(), countTotal);
        }

        public async Task<IList<Lists>> GetAllLists()
        {
            return await this.context.Lists.ToListAsync();
        }

        public async Task<Lists> GetList(int id)
        {
            return await this.context.Lists.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Lists> InsertList(Lists list)
        {
            await this.context.Lists.AddAsync(list);
            await this.context.SaveChangesAsync();
            return list;
        }

        public async Task<Lists> UpdateList(Lists list)
        {
            this.context.Entry<Lists>(list).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return list;
        }
    }
}
