namespace Labratory.Service.IRepositories
{
    using Labratory.Domain.Dtos;
    using Labratory.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IListsRepository
    {

        Task<IList<Lists>> GetAllLists();
        Task<Lists> GetList(int id);
        Task<Lists> InsertList(Lists list);
        Task<Lists> UpdateList(Lists list);
        Task<bool> DeleteList(int id);
        
    }
}
