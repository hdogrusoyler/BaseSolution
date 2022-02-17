using BaseSolution.Project.Presentation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.Project.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        private IPasswordValidator<IdentityUser> passwordValidator;
        private IPasswordHasher<IdentityUser> passwordHasher;
        public UsersController(UserManager<IdentityUser> _userManager, IPasswordValidator<IdentityUser> passValidator, IPasswordHasher<IdentityUser> passHasher)
        {
            userManager = _userManager;
            passwordValidator = passValidator;
            passwordHasher = passHasher;
        }
        [HttpGet]
        public List<IdentityUser> GetList()
        {
            return userManager.Users.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IdentityUser> Get(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<IdentityResult> AddUpdate(string email, string username, string password, string? id)
        {
            if (id != null)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    IdentityUser user = await userManager.FindByIdAsync(id);
                    IdentityResult validPass = await passwordValidator.ValidateAsync(userManager, user, password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, password);

                        IdentityResult result = await userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return result;
                        }
                    }                                        
                }

                return new IdentityResult();
            }
            else
            {
                IdentityResult result =  await userManager.CreateAsync(new IdentityUser() { Email=email, UserName=username}, password);
                
                return result;
            }
        }

        [HttpPost("{id}")]
        public async Task<IdentityResult> Delete(string id)
        {
            var role = userManager.FindByIdAsync(id);
            return await userManager.DeleteAsync(role.Result);
        }
    }
}
