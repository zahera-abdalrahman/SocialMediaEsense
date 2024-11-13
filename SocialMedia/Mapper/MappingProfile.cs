using AutoMapper;
using SocialMedia.Data;
using SocialMedia.DTO;

namespace SocialMedia.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            
            //CreateMap<PostDTO, Post>()
            //    .ForMember(dest => dest.User, opt => opt.Ignore()); 
            //CreateMap<Post, PostDTO>();
        }
    }
}
