using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _env;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IHostEnvironment env, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }

        public IActionResult Index(string searchInp)
        {
            IEnumerable<EmployeeViewModel> mappedEmployees;
            var employees = Enumerable.Empty<Employee>();


            //if (string.IsNullOrEmpty(searchInp))
            //{
            //    var employees = _unitOfWork.EmployeeRepository.GetAll();
            //    mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            //}
            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(searchInp))
                employees = employeeRepo.GetAll();
            else
            {
                employees = employeeRepo.SearchByName(searchInp);
            }
            mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmployees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Employee>().Add(mappedEmp);
                var count = _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Message"] = "An Error Has Occurred During Creation Employee";
                return View(employee);
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = _unitOfWork.Repository<Employee>().Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(mappedEmp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(mappedEmp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);
                _unitOfWork.Repository<Employee>().Update(mappedEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred During Updating The Employee");

                return View(employee);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
             _unitOfWork.Repository<Employee>().Delete(mappedEmp);
            ;

            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _unitOfWork.EmployeeRepository.Delete(id);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred During Deleting The Employee");

                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
