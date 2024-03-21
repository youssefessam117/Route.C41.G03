using Microsoft.AspNetCore.Mvc;
using Route.C41.G03.BLL.Interfaces;


namespace Route.C41.G03PL.Controllers
{
    //ingertiance : DepartnentController is a Controller 
    //Association : DepartnentController has a DepartmentRepository 
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }



        // /Department/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
