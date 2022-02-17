using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.Project.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RoleManager<IdentityRole> roleManager;
        public RolesController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

        [HttpGet]
        public List<IdentityRole> GetList()
        {
            return roleManager.Roles.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IdentityRole> Get(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }

        [HttpPost]
        public async Task<IdentityResult> AddUpdate(string name, string? id)
        {
            if (id == null)
            {
                return await roleManager.CreateAsync(new IdentityRole() { Name = name});
            }
            else
            {
                IdentityRole role = await roleManager.FindByIdAsync(id);
                role.Name = name;
                return await roleManager.UpdateAsync(role);
            }
        }

        [HttpPost("{id}")]
        public async Task<IdentityResult> Delete(string id)
        {
            var role = roleManager.FindByIdAsync(id);
            return await roleManager.DeleteAsync(role.Result);
        }
    }
}
