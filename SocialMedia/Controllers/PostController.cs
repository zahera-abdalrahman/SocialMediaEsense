using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.DTO;
using SocialMedia.Services;
using System.Data;
using System.Net;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IWebHostEnvironment host;

        public PostController(IPostService _postService, IWebHostEnvironment _host)
        {
            postService = _postService;
            host = _host;
        }


        [HttpPost]
        public async Task<IActionResult> Add(PostDTO postDTO)
        {
            try
            {
                await postService.Add(postDTO, host);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                return Ok(await postService.getAll());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("getPostBuUserId")]
        public async Task<IActionResult> getPostBuUserId(string id)
        {
            try
            {
                return Ok(await postService.LoadById(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }


    }
}
