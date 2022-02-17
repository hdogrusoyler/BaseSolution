using BaseSolution.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Project.BusinessLogic.Abstract
{
    public interface ITitleService
    {
        Title GetById(int Id);
        List<Title> GetAll(int page = 1, int pageSize = 0);
        string Add(Title entity);
        string Update(Title entity);
        string Delete(Title entity);
    }
}
