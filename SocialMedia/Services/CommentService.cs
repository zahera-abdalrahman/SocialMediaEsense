using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.DTO;
using SocialMedia.Generic;
using System.Security.Claims;

namespace SocialMedia.Services
{
    public class CommentService : ICommentService
    {
        SocialMediaContext context;
        IMapper mapper;
        private readonly IGeneric<Comment> generic;
        private readonly IAccountService accountService;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CommentService(SocialMediaContext _hrContext, IMapper _mapper
                            , IGeneric<Comment> _generic
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




        public async Task Add(CommentDTO commentDTO)
        {
            Comment comment = new Comment()
            {
                Content = commentDTO.Content,
                postID = commentDTO.postID,
                UserId = commentDTO.UserId,
            };


            await generic.Add(comment);
        }

    }
}
