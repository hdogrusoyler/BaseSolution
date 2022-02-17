using BaseSolution.Project.BusinessLogic.Abstract;
using BaseSolution.Project.DataAccess;
using BaseSolution.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Project.BusinessLogic.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private IUnitOfWork unitOfWork;

        public CategoryManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public Category GetById(int Id)
        {
            Category res = new Category();
            res = unitOfWork.categoryDal.Get(c => c.Id == Id);
            return res;
        }

        public List<Category> GetAll(int page = 1, int pageSize = 0)
        {
            //int page = 1;
            //int pageSize = 0;
            List<Category> res = new List<Category>();
            res = unitOfWork.categoryDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize);//i => i.Photos
            return res;
        }

        public string Add(Category entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.categoryDal.Add(entity);
            return unitOfWork.CommitSaveChanges();
        }
        public string Update(Category entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.categoryDal.Update(entity);
            return unitOfWork.CommitSaveChanges();
        }

        public string Delete(Category entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.categoryDal.Delete(entity);
            return unitOfWork.CommitSaveChanges();
        }
    }
}
