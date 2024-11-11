using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Data
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public string UserId { get; set; }

        public User user { get; set; }
        public int postId { get; set; }
        public Post post { get; set; }

        public bool isLiked{ get; set; }
    }
}
