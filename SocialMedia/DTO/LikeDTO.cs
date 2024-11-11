using AutoMapper;
using SocialMedia.Data;

namespace SocialMedia.DTO
{
    [AutoMap(typeof(Like), ReverseMap = true)]
    public class LikeDTO
    {
        public string UserId { get; set; }

        public int postId { get; set; }
        public UserDTO? User { get; set; }

        public bool isLiked { get; set; }
    }
}
