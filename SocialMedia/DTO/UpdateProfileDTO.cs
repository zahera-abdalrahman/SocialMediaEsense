using System.ComponentModel.DataAnnotations;

namespace SocialMedia.DTO
{
    public class UpdateProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }

        public string? CoverImage { get; set; }

        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
    