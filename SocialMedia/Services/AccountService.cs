using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using SocialMedia.Data;
using SocialMedia.DTO;
using System.Security.Claims;
using static SocialMedia.Services.SendEmail;

namespace SocialMedia.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> identityRole;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISendEmail sendEmail;

        public AccountService(UserManager<User> _userManager, RoleManager<IdentityRole> _identityRole, SignInManager<User> _signInManager
            , IHttpContextAccessor _httpContextAccessor,ISendEmail _sendEmail)
        {
            userManager = _userManager;
            identityRole = _identityRole;
            signInManager = _signInManager;
            httpContextAccessor = _httpContextAccessor;
            sendEmail = _sendEmail;
        }
 
        public async Task<bool> SignUp(SignUpDTO signUpDTO)
        {
            bool IsSuccess = true;


            var existingUser = await userManager.FindByNameAsync(signUpDTO.Email);
            if (existingUser != null)
            {
                IsSuccess = false;
                return IsSuccess;
            }


            User newUser = new User()
            {
                UserName = signUpDTO.Email,
                FirstName = signUpDTO.FirstName,
                LastName = signUpDTO.LastName,
                Email = signUpDTO.Email,
                DOB = signUpDTO.DOB,
                Gender = signUpDTO.Gender,
                IsVerified = false,
            };

            var createUserResult = await userManager.CreateAsync(newUser, signUpDTO.Password);

            if (createUserResult.Succeeded)
            {
                //var roleResult = await userManager.AddToRoleAsync(newUser, signUpDTO.RoleName);

                var roleResult = await userManager.AddToRoleAsync(newUser, "User");

                if (!roleResult.Succeeded)
                {
                    await userManager.DeleteAsync(newUser);
                    IsSuccess = false;
                }

                else
                {
                    string randomCode = GenerateRandom.GenerateRandomAlphanumericString(8);
                    newUser.ConfirmationCode = randomCode;

                    var updateResult = await userManager.UpdateAsync(newUser);
                    if (!updateResult.Succeeded)
                    {
                        IsSuccess = false;
                    }

                    await sendEmail.SendEmailAsync(newUser.UserName, newUser.FirstName, randomCode);
                }

            }
            return IsSuccess;
        }

        public async Task<SignInResult> Login(LoginInDTO loginInDTO)
        {
            var result = await signInManager.PasswordSignInAsync(loginInDTO.Email, loginInDTO.Password, false, false);
            return result;
        }

        public async Task<IdentityResult> AddRole(RoleDTO roleDTO)
        {
            IdentityRole identity = new IdentityRole()
            {
                //Name = roleDTO.RoleName
                Name="User"
            };

            return await identityRole.CreateAsync(identity);
        }


        public async Task<IList<string>> getUserRules(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return await userManager.GetRolesAsync(user);
        }

        public async Task<User> getUserInfo(string id)
        {
            var user=await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return user; 
        }


        public async Task<User> getloggedInUser(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }

            return user;
        }


        public async Task<bool> UpdateUserProfile(UpdateProfileDTO updateProfileDTO,string id)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(updateProfileDTO.FirstName))
            {
                user.FirstName = updateProfileDTO.FirstName;
            }
            if (!string.IsNullOrEmpty(updateProfileDTO.LastName))
            {
                user.LastName = updateProfileDTO.LastName;
            }
            if (!string.IsNullOrEmpty(updateProfileDTO.ProfileImage))
            {
                user.ProfileImage = updateProfileDTO.ProfileImage;
            }
            if (!string.IsNullOrEmpty(updateProfileDTO.CoverImage))
            {
                user.CoverImage = updateProfileDTO.CoverImage;
            }

            if (!string.IsNullOrEmpty(updateProfileDTO.NewPassword) && !string.IsNullOrEmpty(updateProfileDTO.CurrentPassword))
            {
                var changePasswordResult = await userManager.ChangePasswordAsync(user, updateProfileDTO.CurrentPassword, updateProfileDTO.NewPassword);
            }


            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> updateUserCode(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            user.IsVerified = true;
            var updateResult = await userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                return true;
            }
            else {
                return false;
            }

        }


    }
}
