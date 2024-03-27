using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.Dal.Models;
using System;

namespace Route.C41.G03PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment env;
        //private readonly IDepartmentRepository departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment env /*,IDepartmentRepository departmentRepository*/)
        {
            this.employeeRepository = employeeRepository;
            this.env = env;
            //this.departmentRepository = departmentRepository;
        }



        // /Employee/Index
        public IActionResult Index()
        {
            // TempData.Keep(); if i want to keep tempdata value from previous req 
            // Binding Through Views Dictionary : transfer Data from action to view 

            // 1. viewData is a dictionary type property (introduced in asp.net framework 3.5)
            //     => it helps us to transfer th data from controller[action] to view 

            ViewData["Message"] = "Hello ViewData";

            // 2. ViewBag is a dynamic Type Property (introduced in asp.net frameWork 4.0 based on dynamic)
            //     => it helps us to transfer th data from controller[action] to view 

            ViewBag.message = "Hello ViewBag";

            var employees = employeeRepository.GetAll();
            // Binding Through Model
            return View(employees);
        }

        // /Employee/Create
        //[HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid) // Server side Validation
            {
                var count = employeeRepository.Add(employee);
                if (count > 0)
                    TempData["Message"] = "Employee is Created Successfully";
                else
                    TempData["Message"] = "Ann Error Has Occured , Emplyee not Created ";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var employee = employeeRepository.Get(id.Value);

            if (employee is null)
            {
                return NotFound();
            }
            return View(viewName, employee);
        }

        // /Employee/Edit/10 
        // /Employee/Edit
        public IActionResult Edit(int? id)
        {
            
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            try
            {
                employeeRepository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1 log Exption
                // 2 Friendly Message

                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Ocurred During Updating The Employee");
                return View(employee);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Ocurred During Deleting The Employee");
                return View(employee);
            }
        }
    }
}
