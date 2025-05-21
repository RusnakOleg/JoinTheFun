using JoinTheFun.BLL.DTO.Folowers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IFollowService
    {
        Task<bool> IsFollowingAsync(string followerId, string followedId);
        Task FollowAsync(FollowDto dto);
        Task UnfollowAsync(FollowDto dto);
    }
}
