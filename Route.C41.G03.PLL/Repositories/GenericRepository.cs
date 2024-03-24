using Microsoft.EntityFrameworkCore;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.Dal.Data;
using Route.C41.G03.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G03.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) // ask clr for creating object from ApplicationDbContext
        {
            _dbContext = dbContext;
        }
        public int Add(T entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();

        }

        public int Delete(T entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            ///var T = _dbContext.Ts.Local.Where(d=>d.Id == id).FirstOrDefault();
            ///if (T == null)
            ///{
            ///    T = _dbContext.Ts.Where(T => T.Id == id).FirstOrDefault();
            ///}
            ///return T;
            //return _dbContext.Ts.Find(id);
            return _dbContext.Find<T>(id); // ef core 3.1 New Feature
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking().ToList();
        }

        public int Update(T entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
