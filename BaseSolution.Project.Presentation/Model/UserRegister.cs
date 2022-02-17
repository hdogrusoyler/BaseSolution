using Microsoft.AspNetCore.Identity;

namespace BaseSolution.Project.Presentation.Model
{
    public class UserRegister
    {
        public IdentityUser User { get; set; }
        public string Password { get; set; }
    }
}
