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


        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            return Ok(await likeService.getLikes());
        }

        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] LikeDTO likeDTO)
        {
            bool isAdded = await likeService.Add(likeDTO);

            if (isAdded)
            {
                return Ok(new { message = "Like added successfully." });
            }
            else
            {
                return Conflict(new { message = "You have already liked this post." }); 
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id should be greater than 0");
            }

            await likeService.Delete(id);
            return Ok(new { message = "Comment deleted successfully." });
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
