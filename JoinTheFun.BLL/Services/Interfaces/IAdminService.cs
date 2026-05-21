using JoinTheFun.BLL.DTO.Profiles;

namespace JoinTheFun.BLL.Services.Interfaces;

public interface IAdminService
{
    Task<IEnumerable<ProfileDto>> GetAllUsersWithProfilesAsync();
    Task<bool> BanUserAsync(string userId, int days);
    Task<bool> UnbanUserAsync(string userId);
}