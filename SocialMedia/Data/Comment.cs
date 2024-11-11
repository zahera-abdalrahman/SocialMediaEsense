using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Data
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [StringLength(50)]
        public string Content { get; set; }

        public string UserId { get; set; }


        public User user { get; set; }
        public int postID { get; set; }
        public Post post { get; set; }
    }
}
