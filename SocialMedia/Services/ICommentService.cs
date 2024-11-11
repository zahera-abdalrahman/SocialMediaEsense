using SocialMedia.DTO;

namespace SocialMedia.Services
{
    public interface ICommentService
    {
        Task Add(CommentDTO commentDTO);
    }
}