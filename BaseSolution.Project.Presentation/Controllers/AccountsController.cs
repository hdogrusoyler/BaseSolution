using BaseSolution.Project.Presentation.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaseSolution.Project.Presentation.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        IConfiguration configuration;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        private SignInManager<IdentityUser> signinManager;
        public AccountsController(IConfiguration _configuration, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, SignInManager<IdentityUser> _signinManager)
        {
            configuration = _configuration;
            userManager = _userManager;
            roleManager = _roleManager;
            signinManager = _signinManager;
        }

        [HttpPost]
        [Route("login")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] UserLogin UserLogin)
        {
            //IdentityRole role = await roleManager.FindByIdAsync();
            IdentityUser user = await userManager.FindByEmailAsync(UserLogin.email);
            if (user != null)
            {           
                await signinManager.SignOutAsync();
                var result = await signinManager.PasswordSignInAsync(user, UserLogin.password, false, false);
                if (result.Succeeded)
                {
                    IList<string> roles = await userManager.GetRolesAsync(user);
                    UserToken userToken = new UserToken()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = roles
                    };
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    byte[] key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);
                    SigningCredentials signincredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
                    //var res = Enum.GetName(typeof(string), userToken.Role);                    
                    ClaimsIdentity claimIdentity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, userToken.Role[0]), //JsonConvert.SerializeObject(userToken.Role)
                        new Claim("id", userToken.Id.ToString()),
                        new Claim(ClaimTypes.GivenName, userToken.UserName),
                        new Claim(ClaimTypes.Email, userToken.Email)
                    });
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = claimIdentity,
                        Expires = DateTime.Now.AddDays(1),
                        SigningCredentials = signincredentials
                    };
                    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                    userToken.Token = tokenHandler.WriteToken(token);

                    return Ok(userToken);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
