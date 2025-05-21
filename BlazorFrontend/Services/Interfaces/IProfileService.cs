using BlazorFrontend.DTO.Profiles;

namespace BlazorFrontend.Services.Interfaces
{
    public interface IProfileService
    {
        Task<List<ProfileDto>> GetAllAsync();

        Task<List<ProfileDto>> GetByCityAsync(string city);
        Task<List<ProfileDto>> GetByInterestIdAsync(int interestId);
    }

}
