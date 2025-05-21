using BlazorFrontend.DTO.Interests;
using BlazorFrontend.DTO.UserInterest;

namespace BlazorFrontend.Services.Interfaces
{
    public interface IUserInterestService
    {
        Task<List<InterestDto>> GetByProfileIdAsync(int profileId);
        Task<bool> AddAsync(AddUserInterestDto dto);
        Task<bool> RemoveAsync(AddUserInterestDto dto);
    }
}
