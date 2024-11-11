using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Data;
using SocialMedia.DTO;
using SocialMedia.Generic;
using System.Security.Claims;

namespace SocialMedia.Services
{
    public class PostService : IPostService
    {
        SocialMediaContext context;
        IMapper mapper;
        IGeneric<Post> generic;
        private readonly IAccountService accountService;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PostService(SocialMediaContext _hrContext, IMapper _mapper
                            , IGeneric<Post> _generic
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




        public async Task Add(PostDTO postDTO, IWebHostEnvironment host)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //string? contentImagePath = null;
            //if (postDTO.File != null)
            //{
            //    contentImagePath = ProcessUploadedFile(postDTO.File, host);
            //}

            //Post post = mapper.Map<Post>(postDTO);

            Post post = new Post()
            {
                UserId = postDTO.UserId,
                ContentImage = postDTO.ContentImage,
                ContentText = postDTO.contentText,
                CreatedAt = DateTime.Now,

            };

            await generic.Add(post);
        }


        public async Task<List<PostDTO>> LoadById(string id)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Post> posts = await context.posts
                 .Include(u => u.User)
        .Include(l => l.like)
            .ThenInclude(l => l.user)
        .Include(c => c.comments)
            .ThenInclude(c => c.user)
                .Where(e => e.UserId == id)
                .ToListAsync();

            List<PostDTO> postDTO = mapper.Map<List<PostDTO>>(posts);

            return postDTO;

        }

        public async Task<List<PostDTO>> getAll()
        {
            var posts = await context.posts
         
        .Include(u => u.User)
        .Include(l => l.like)
            .ThenInclude(l => l.user)
        .Include(c => c.comments)
            .ThenInclude(c => c.user)
            .OrderByDescending(d => d.CreatedAt)
        .ToListAsync();


            List<PostDTO> postDTOs = mapper.Map<List<PostDTO>>(posts);

            return postDTOs;
        }



    }
}
