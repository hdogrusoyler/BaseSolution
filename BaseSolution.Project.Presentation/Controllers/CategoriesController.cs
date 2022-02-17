using BaseSolution.Project.BusinessLogic.Abstract;
using BaseSolution.Project.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.Project.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService categoryService;

        public CategoriesController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [HttpGet]
        public List<Category> GetList()
        {
            return categoryService.GetAll();
        }

        [HttpGet("{id}")]
        public Category Get(int Id)
        {
            return categoryService.GetById(Id);
        }

        [HttpPost]
        public string AddUpdate([FromBody] Category category)
        {
            if (category.Id > 0)
            {
                return categoryService.Update(category);
            }
            else
            {
                return categoryService.Add(category);
            }
        }

        [HttpPost("{id}")]
        public string Delete(int Id)
        {
            return categoryService.Delete(new Category { Id = Id });
        }
    }
}
