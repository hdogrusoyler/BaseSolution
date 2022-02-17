using Microsoft.AspNetCore.Identity;

namespace BaseSolution.Project.Presentation.Model
{
    public class UserRole
    {
        public IdentityUser User { get; set; }
        public IdentityRole Role { get; set; }
    }
}
