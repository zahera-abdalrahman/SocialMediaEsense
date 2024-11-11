using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Data
{
    public class User:IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }


        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        public string? ProfileImage { get; set; } 

        public string? CoverImage { get; set; }


        public bool IsVerified { get; set; }

        public string? ConfirmationCode { get; set; }

        public List<Post> posts { get; set; }
        public List<Like> likes { get; set; }
        public List<Comment> comments { get; set; }
    }
}
