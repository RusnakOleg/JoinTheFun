using JoinTheFun.BLL.DTO.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto?> GetByUserIdAsync(string userId);
        Task UpdateAsync(string userId, UpdateProfileDto dto);
    }
}
