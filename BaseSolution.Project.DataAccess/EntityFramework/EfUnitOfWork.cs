using BaseSolution.Core.DataAccess;
using BaseSolution.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Project.DataAccess.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext context;
        private IDbContextTransaction transaction;
        public ITitleRepository titleDal { get; set; }
        public ICategoryRepository categoryDal { get; set; }

        public EfUnitOfWork(DataContext _context, ITitleRepository _titleDal, ICategoryRepository _categoryDal)
        {
            context = _context;
            titleDal = _titleDal;
            categoryDal = _categoryDal;
        }
        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }
        public string CommitSaveChanges()
        {
            string result = "";
            try
            {
                result = Save().ToString();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                result = String.IsNullOrEmpty(e.InnerException?.Message) ? e.Message : e.InnerException.Message;
                //throw;
            }
            finally
            {
                transaction.Dispose();
                Dispose();
            }
            return result;
        }
        public int Save()
        {
            return context.SaveChanges();
        }
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();                   
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
