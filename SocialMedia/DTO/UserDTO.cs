using AutoMapper;
using SocialMedia.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.DTO
{
    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserDTO
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DOB { get; set; }
        public string Gender { get; set; }

        public string? ProfileImage { get; set; }

        public string? CoverImage { get; set; }


    }
}
