using JoinTheFun.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories.Interfaces
{
    public interface IUserInterestRepository
    {
        Task<IEnumerable<UserInterest>> GetInterestsByProfileIdAsync(int profileId);
        Task AddAsync(UserInterest interest);
        Task RemoveAsync(UserInterest interest);
    }
}
