using AutoMapper;
using SocialMedia.Data;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.DTO
{
    [AutoMap(typeof(Post), ReverseMap = true)]
    public class PostDTO
    {

        public int postId{ get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        public string? contentText { get; set; }
        
        public string? ContentImage { get; set; }

        public UserDTO? User { get; set; }
        public List<LikeDTO>? like { get; set; }
        public List<CommentDTO>? Comments { get; set; }

        public string UserId { get; set; }
    }
}
