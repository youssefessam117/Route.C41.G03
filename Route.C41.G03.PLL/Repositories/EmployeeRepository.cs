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
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) // ask clr for creating object from ApplicationDbContext
        {
            _dbContext = dbContext;
        }
        public int Add(Employee entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();

        }

        public int Delete(Employee entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            ///var Employee = _dbContext.Employees.Local.Where(d=>d.Id == id).FirstOrDefault();
            ///if (Employee == null)
            ///{
            ///    Employee = _dbContext.Employees.Where(Employee => Employee.Id == id).FirstOrDefault();
            ///}
            ///return Employee;
            //return _dbContext.Employees.Find(id);
            return _dbContext.Find<Employee>(id); // ef core 3.1 New Feature
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.AsNoTracking().ToList();
        }

        public int Update(Employee entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
