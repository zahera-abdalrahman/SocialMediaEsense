using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Data
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string? ContentText { get; set; }

        public string? ContentImage { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
        public List<Comment> comments { get; set; }
        public List<Like> like { get; set; }
    }
}
