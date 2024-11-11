using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.DTO;
using SocialMedia.Generic;
using System.Security.Claims;

namespace SocialMedia.Services
{
    public class LikeService : ILikeService
    {

        SocialMediaContext context;
        IMapper mapper;
        IGeneric<Like> generic;
        private readonly IAccountService accountService;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LikeService(SocialMediaContext _hrContext, IMapper _mapper
                            , IGeneric<Like> _generic
                            , IAccountService _accountService,
                                UserManager<User> _userManager
                                , IHttpContextAccessor _httpContextAccessor)
        {
            context = _hrContext;
            mapper = _mapper;
            generic = _generic;
            accountService = _accountService;
            userManager = _userManager;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<List<LikeDTO>> getLikes()
        {
            var likes = await context.likes.Include(c=>c.user).ToListAsync();

            List<LikeDTO> likeDTOs= mapper.Map<List<LikeDTO>>(likes);

            return likeDTOs;
        }

        public async Task<bool> Add(LikeDTO likeDTO)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingLike = await context.likes
                .FirstOrDefaultAsync(l => l.UserId == likeDTO.UserId && l.postId == likeDTO.postId);

            if (existingLike != null)
            {
                return false;
            }

            Like like = new Like()
            {
                UserId = likeDTO.UserId,
                postId=likeDTO.postId
            };

            await generic.Add(like);
            return true;
        }


        public async Task<bool> ToggleLike(int postId, string userId)
        {
            var post = await context.posts.FindAsync(postId);
            if (post == null)
            {
                return false;
            }

            var cuurentLike = await context.likes
                .FirstOrDefaultAsync(l => l.postId == postId && l.UserId == userId);

            if (cuurentLike != null)
            {
                cuurentLike.isLiked = !cuurentLike.isLiked;
                await context.SaveChangesAsync();
                return cuurentLike.isLiked;
            }

            else
            {
                var like = new Like
                {
                    postId = postId,
                    UserId = userId,
                    isLiked = true
                };
                await context.likes.AddAsync(like);
                await context.SaveChangesAsync();
                return true;
            }


        }



        public async Task Delete(int Id)
        {
            await generic.Delete(Id);
        }


    }
}
