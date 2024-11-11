using AutoMapper;
using SocialMedia.Data;

namespace SocialMedia.DTO
{
    [AutoMap(typeof(Comment), ReverseMap = true)]
    public class CommentDTO
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public int postID { get; set; }
        public UserDTO? user { get; set; }
    }
}
