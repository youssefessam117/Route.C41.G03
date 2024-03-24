using Route.C41.G03.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G03.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //if the interface stays empty we don't need it
        //but we leave it like this 
        //so if we need to implement any method that is related to the interface in the future
        //and to know that we need to make that interface 

        public IQueryable<Employee> GetEmployeesByAddress(string address);
    }
}
