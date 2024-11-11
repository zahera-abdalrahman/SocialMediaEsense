using SocialMedia.DTO;

namespace SocialMedia.Services
{
    public interface ILikeService
    {
        Task<List<LikeDTO>> getLikes();
        Task<bool> Add(LikeDTO likeDTO);
        Task Delete(int Id);

        Task<bool> ToggleLike(int postId, string userId);

    }
}