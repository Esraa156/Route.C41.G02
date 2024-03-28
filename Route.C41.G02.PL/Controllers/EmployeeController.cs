using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IMapper mapper, IEmployeeRepository EmployeeRepository, IWebHostEnvironment environment)
        {
            this.mapper = mapper;
            _EmployeeRepository = EmployeeRepository;
            _environment = environment;
        }

        public IActionResult Index(string searchInp)
        {
            var Employees = Enumerable.Empty<Employee>();

            if (searchInp.IsNullOrEmpty())
            {
                 Employees = _EmployeeRepository.GetAll();

            }
            else
            {
                
                 Employees = _EmployeeRepository.SearchByName(searchInp.ToLower());
            
            }
            var Mappedemp = mapper.Map<IEnumerable<Employee>, IEnumerable< EmployeeViewModel> >(Employees);

            return View(Mappedemp);



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
        public IActionResult Create(EmployeeViewModel employee)
        {

            if (ModelState.IsValid)
            {

                var Mappedemp = mapper.Map<EmployeeViewModel, Employee>(employee);
                int c = _EmployeeRepository.Add(Mappedemp);
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employee)
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
                var Mappedemp = mapper.Map<EmployeeViewModel, Employee>(employee);


                _EmployeeRepository.Update(Mappedemp);
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
            var Mappedemp = mapper.Map< Employee, EmployeeViewModel>(Emp);

            if (Emp is null)
            {
                return NotFound(); //404

            }

            return View(ViewName, Mappedemp);

        }

        [HttpGet]
        public IActionResult Delete(int id) {

            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employee)
        {
            try
            {
                var Mappedemp = mapper.Map<EmployeeViewModel, Employee>(employee);

                _EmployeeRepository.Delete(Mappedemp);
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
    
