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

        public async Task<IList<Lists>> GetAllLists()
        {
            try
            {

            return await this.context.Lists.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
