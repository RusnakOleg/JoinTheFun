using JoinTheFun.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile?> GetByUserIdAsync(string userId);
        Task AddAsync(Profile profile);
        Task UpdateAsync(Profile profile);

        Task<IEnumerable<Profile>> GetByCityAsync(string city);
        Task<IEnumerable<Profile>> GetByInterestIdAsync(int interestId);

    }
}
