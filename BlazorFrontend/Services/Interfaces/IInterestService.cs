using BlazorFrontend.DTO.Interests;

namespace BlazorFrontend.Services.Interfaces
{
        public interface IInterestService
        {
            Task<List<InterestDto>> GetAllAsync();
            Task<bool> CreateAsync(CreateInterestDto dto);
        }


}
