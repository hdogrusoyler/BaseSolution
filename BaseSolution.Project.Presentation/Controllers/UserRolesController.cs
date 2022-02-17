using BaseSolution.Project.Presentation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.Project.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;  
        private IPasswordValidator<IdentityUser> passwordValidator;
        private IPasswordHasher<IdentityUser> passwordHasher;
        public UserRolesController(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, IPasswordValidator<IdentityUser> passValidator, IPasswordHasher<IdentityUser> passHasher)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            passwordValidator = passValidator;
            passwordHasher = passHasher;
        }

        [HttpGet("GetRole/{userId}")]
        public async Task<List<string>> GetRoleByUserList(string userId)
        {
            IList<string> result = new List<string>();

            if (userId != null)
            {
                IdentityUser user = await userManager.FindByIdAsync(userId);
                result = await userManager.GetRolesAsync(user);
            }

            return result.ToList();
        }

        [HttpGet("GetUser/{roleName}")]
        public async Task<List<IdentityUser>> GetUserByRoleList(string roleName)
        {
            IList<IdentityUser> result = new List<IdentityUser>();

            if (roleName != null)
            {
                result = await userManager.GetUsersInRoleAsync(roleName);
            }

            return result.ToList();
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IdentityResult> Add(string userId, string roleName)
        {
            IdentityResult result = new IdentityResult();

            if (userId != null)
            {
                IdentityUser user = await userManager.FindByIdAsync(userId);
                result = await userManager.AddToRoleAsync(user, roleName);
            }

            return result;
        }

        [Route("Delete")]
        [HttpPost]
        public async Task<IdentityResult> Delete(string userId, string roleName)
        {
            IdentityResult result = new IdentityResult();

            if (userId != null)
            {
                var res = roleManager.FindByNameAsync(userId);
                if (res != null)
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    result = await userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            return result;
        }
    }
}
