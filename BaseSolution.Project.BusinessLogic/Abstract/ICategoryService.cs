using BaseSolution.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Project.BusinessLogic.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int Id);
        List<Category> GetAll(int page = 1, int pageSize = 0);
        string Add(Category entity);
        string Update(Category entity);
        string Delete(Category entity);
    }
}
