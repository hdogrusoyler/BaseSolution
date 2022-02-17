using BaseSolution.Core.DataAccess;
using BaseSolution.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Project.DataAccess
{
    public interface IUnitOfWork
    {
        ITitleRepository titleDal { get; set; }
        ICategoryRepository categoryDal { get; set; }

        void BeginTransaction();
        string CommitSaveChanges();
        int Save();
        void Dispose();
    }
}
