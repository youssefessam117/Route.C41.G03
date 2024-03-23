using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.Dal.Models;
using System;


namespace Route.C41.G03PL.Controllers
{
    //ingertiance : DepartnentController is a Controller 
    //Association : DepartnentController has a DepartmentRepository 
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment env;

        public DepartmentController(IDepartmentRepository departmentRepository, IWebHostEnvironment env)
        {
            _departmentRepository = departmentRepository;
            this.env = env;
        }



        // /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        // /Department/Create
        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // Server side Validation
            {
               var count = _departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        public IActionResult Details(int? id , string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var department = _departmentRepository.Get(id.Value);

            if (department is null)
            {
                return NotFound();
            }
            return View(viewName,department);
        }

        // /Department/Edit/10 
        // /Department/Edit
        public IActionResult Edit(int? id)
        {
           ///if (id is null)
           ///{
           ///    return BadRequest();
           ///}
           ///var department = _departmentRepository.Get(id.Value);
           ///if (department is null)
           ///{
           ///    return NotFound();
           ///}
           ///return View(department);
            return Details(id,"Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            try
            {
                _departmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1 log Exption
                // 2 Friendly Message

                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Ocurred During Updating The Department");
                return View(department);    
            }
        }
    }
}
