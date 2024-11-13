using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Data;
using SocialMedia.DTO;
using SocialMedia.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment host;

        public AccountController(IAccountService _accountService, UserManager<User> _userManager, IWebHostEnvironment _host)
        {
            userManager = _userManager;
            host = _host;
            accountService = _accountService;
        }


        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            try
            {
                var result = await accountService.SignUp(signUpDTO);

                if (result)
                {
                    return Ok(new { message = "User created successfully" });
                }
                else
                {
                    return BadRequest(new { message = "This email is already registered" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }




        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginInDTO loginInDTO)
        {
            try
            {
                var result = await accountService.Login(loginInDTO);

                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(loginInDTO.Email);
                    if (user == null)
                    {
                        return Unauthorized( new { StatusCode="UserNotFound",  message = "User not found" });
                    }
                    if (!user.IsVerified)
                    {
                        return BadRequest(new { StatusCode = "NotVarified", message = "Email not verified. Please verify your account." });
                    }

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, loginInDTO.Email),
                        new Claim("uniquevalue",Guid.NewGuid().ToString()),
                    };

                    var roles = await accountService.getUserRules(loginInDTO.Email);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySuperSecureKeyWith32Characters"));

                    var token = new JwtSecurityToken(
                        issuer: "http://localhost",
                        audience: "User",
                        expires: DateTime.Now.AddDays(15),
                        claims: claims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                    });
                }
                else
                {
                    return Unauthorized(new { StatusCode = "Login_Failed", message = "Invalid email or password." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }

        }


        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(RoleDTO roleDTO)
        {
            try
            {
                var result = await accountService.AddRole(roleDTO);
                if (result.Succeeded)
                {
                    return StatusCode((int)HttpStatusCode.OK, "Role added sucessfully");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "error while adding the role");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        [Route("userInfo")]
        public async Task<ActionResult<User>> GetUserProfile(string id)
        {
            try
            {
                var userProfile = await accountService.getUserInfo(id);
                if (userProfile == null)
                {
                    return NotFound(new { message = "User profile not found" });
                }
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("finduser")]
        public async Task<ActionResult<User>> getloggedInUser(string username)
        {
            try
            {
                var userProfile = await accountService.getloggedInUser(username);
                if (userProfile == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }




        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile(UpdateProfileDTO updateProfileDTO, string id)
        {
            try
            {
                var result = await accountService.UpdateUserProfile(updateProfileDTO, id);
                if (!result)
                {
                    return BadRequest(new { message = "Failed to update profile." });
                }

                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }


        [HttpPut]
        [Route("updateVerficate")]
        public async Task<IActionResult> updateAccountVerficate(string username)
        {
            try
            {
                var result = await accountService.updateUserCode(username);

                if (result)
                {
                    return Ok(new { message = "User updated successfully" });
                }
                else
                {
                    return BadRequest(new { message = "code error" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }

        }


    }
}
