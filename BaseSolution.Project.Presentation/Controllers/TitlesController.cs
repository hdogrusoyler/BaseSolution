using BaseSolution.Project.BusinessLogic.Abstract;
using BaseSolution.Project.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.Project.Presentation.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private ITitleService titleService;

        public TitlesController(ITitleService _titleService)
        {
            titleService = _titleService;
        }

        [AllowAnonymous]
        [HttpGet]
        public List<Title> GetList()
        {
            return titleService.GetAll();
        }

        [HttpGet("{id}")]
        public Title Get(int Id)
        {
            return titleService.GetById(Id);
        }

        [HttpPost]
        public string AddUpdate([FromBody] Title title)
        {
            if (title.Id > 0)
            {
                return titleService.Update(title);
            }
            else
            {
                return titleService.Add(title);
            }
        }

        [HttpPost("{id}")]
        public string Delete(int Id)
        {
            return titleService.Delete(new Title { Id = Id });
        }
    }
}
