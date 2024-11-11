using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.DTO;
using SocialMedia.Services;
using System.Net;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            commentService = _commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentDTO commentDTO)
        {
            try
            {
                await commentService.Add(commentDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }



    }
}
