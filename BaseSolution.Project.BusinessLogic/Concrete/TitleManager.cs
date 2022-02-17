using BaseSolution.Project.BusinessLogic.Abstract;
using BaseSolution.Project.DataAccess;
using BaseSolution.Project.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Project.BusinessLogic.Concrete
{
    public class TitleManager : ITitleService
    {
        private IUnitOfWork unitOfWork;

        public TitleManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public Title GetById(int Id)
        {
            Title res = new Title();
            res = unitOfWork.titleDal.Get(c => c.Id == Id);
            return res;
        }

        public List<Title> GetAll(int page = 1, int pageSize = 0)
        {
            //int page = 1;
            //int pageSize = 0;
            List<Title> res = new List<Title>();
            res = unitOfWork.titleDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(c => c.Category));//i => i.Photos
            return res;
        }

        public string Add(Title entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.titleDal.Add(entity);
            return unitOfWork.CommitSaveChanges();
        }
        public string Update(Title entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.titleDal.Update(entity);
            return unitOfWork.CommitSaveChanges();
        }

        public string Delete(Title entity)
        {
            unitOfWork.BeginTransaction();
            unitOfWork.titleDal.Delete(entity);
            return unitOfWork.CommitSaveChanges();
        }
    }
}