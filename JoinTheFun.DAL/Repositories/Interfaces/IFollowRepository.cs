using JoinTheFun.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Repositories.Interfaces
{
    public interface IFollowRepository
    {
        Task<bool> IsFollowingAsync(string followerId, string followedId);
        Task AddAsync(Follow follow);
        Task RemoveAsync(Follow follow);
    }
}
