using JoinTheFun.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories.Interfaces
{
    public interface IInterestRepository
    {
        Task<IEnumerable<Interest>> GetAllAsync();
        Task<Interest?> GetByIdAsync(int id);
    }
}
