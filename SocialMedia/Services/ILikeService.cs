using SocialMedia.DTO;

namespace SocialMedia.Services
{
    public interface ILikeService
    {

        Task<bool> ToggleLike(int postId, string userId);

    }
}