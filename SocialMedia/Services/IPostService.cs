using SocialMedia.DTO;

namespace SocialMedia.Services
{
    public interface IPostService
    {
        Task Add(PostDTO postDTO, IWebHostEnvironment host);

        Task<List<PostDTO>> getAll();

        Task<List<PostDTO>> LoadById(string id);
    }
}