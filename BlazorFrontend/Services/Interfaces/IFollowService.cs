using BlazorFrontend.DTO.Folowers;

namespace BlazorFrontend.Services.Interfaces
{
    public interface IFollowService
    {
        Task<bool> IsFollowingAsync(string followerId, string followedId);
        Task<bool> FollowAsync(FollowDto dto);
        Task<bool> UnfollowAsync(FollowDto dto);
    }
}
