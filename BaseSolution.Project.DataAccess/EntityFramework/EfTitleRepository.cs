using BaseSolution.Core.DataAccess.EntityFramework;
using BaseSolution.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Project.DataAccess.EntityFramework
{
    public class EfTitleRepository : EfBaseRepository<Title, DataContext>, ITitleRepository
    {
        public EfTitleRepository(DataContext dbContext) : base(dbContext)
        {

        }
    }
}
