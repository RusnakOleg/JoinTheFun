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
        Task<IEnumerable<ProfileDto>> GetByCityAsync(string city);
        Task<IEnumerable<ProfileDto>> GetByInterestIdAsync(int interestId);
        Task AddAsync(UpdateProfileDto dto, string userId); // створення нового профілю
        Task UpdateAsync(UpdateProfileDto dto, string userId);
    }

}
