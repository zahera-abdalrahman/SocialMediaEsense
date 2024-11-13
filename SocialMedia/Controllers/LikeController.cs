using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.DTO;
using SocialMedia.Services;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService _likeService)
        {
            likeService = _likeService;
        }

        [HttpPost]
        [Route("ToggleLike")]
        public async Task<IActionResult> ToggleLike(int postId, string userId)
        {
            var isLiked = await likeService.ToggleLike(postId, userId);
            return Ok(isLiked);
        }





    }
}
