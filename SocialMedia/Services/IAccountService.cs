using Microsoft.AspNetCore.Identity;
using SocialMedia.Data;
using SocialMedia.DTO;

namespace SocialMedia.Services
{
    public interface IAccountService
    {
        Task<bool> SignUp(SignUpDTO signUpDTO);

        Task<SignInResult> Login(LoginInDTO loginInDTO);

        Task<IdentityResult> AddRole(RoleDTO roleDTO);

        Task<IList<string>> getUserRules(string email);


        Task<User> getUserInfo(string id);

        Task<User> getloggedInUser(string username);

        Task<bool> UpdateUserProfile(UpdateProfileDTO updateProfileDTO, string id);
        Task <bool> updateUserCode(string username);
    }
}