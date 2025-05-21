using JoinTheFun.BLL.DTO.Interests;
using JoinTheFun.BLL.DTO.UserInterest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IUserInterestService
    {
        Task<IEnumerable<InterestDto>> GetByProfileIdAsync(int profileId);
        Task AddAsync(AddUserInterestDto dto);
        Task RemoveAsync(AddUserInterestDto dto);
    }
}
