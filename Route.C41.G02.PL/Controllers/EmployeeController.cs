using Castle.Core.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using System;
using System.Drawing;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeRepository EmployeeRepository, IWebHostEnvironment environment)
        {
            _EmployeeRepository = EmployeeRepository;
            _environment = environment;
        }

        public IActionResult Index(string searchInp)
        {

            if (searchInp.IsNullOrEmpty())
            {
                var Employees = _EmployeeRepository.GetAll();
                return View(Employees);

            }
            else
            {
                
                var employee= _EmployeeRepository.SearchByName(searchInp.ToLower());
                return View(employee);
            
            }

            //1.ViewData

            ViewData["Message"] = "Hello ViewData";

            //2.ViewBag
            ViewBag.Message = "Hello ViewBag";

        }
        [HttpGet]
        public IActionResult Create() {

            return View();

        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                int c = _EmployeeRepository.Add(employee);
                if (c > 0)
                {
                    TempData["Message"] = "Employee Created Successfully ";

                    //TempData

                }
                else
                {
                    TempData["Message"] = "An Error Has Occured During Creation Employee ";

                }
                return RedirectToAction(nameof(Index));
            }


            return View(employee);

        }
    
        [HttpGet]
        public IActionResult Edit(int? id)
        {




            return Details(id, "Edit");

        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {

            if (id != employee.Id)
            {
                return BadRequest("An Error Ya Esraa !");

            }
            if (!ModelState.IsValid)
            {
                return View(employee);

            }
            try
            {

                _EmployeeRepository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Ex)
            {
                //1. Log Exception
                //2. Friendly message
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "An Error Has Ocuured During updating The Employee ");
                }
                return View(employee);

            }
        }


        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); //400
            }
            var Emp = _EmployeeRepository.GetById(id.Value);
            if (Emp is null)
            {
                return NotFound(); //404

            }

            return View(ViewName, Emp);

        }

        [HttpGet]
        public IActionResult Delete(int id) {

            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _EmployeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));


            }

            catch (Exception Ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Has Ocuured During updating The Employee ");

                }


            }
            return View(employee);
        } }




        }
    
