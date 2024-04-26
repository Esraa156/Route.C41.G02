using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using Route.C41.G02.PL.Helpers;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
	[Authorize]
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
        public async Task<IActionResult> Index(string searchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(searchInput))
                employees =await employeeRepo.GetAllAsync();
            else
                employees = employeeRepo.SearchByName(searchInput);

            //var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(_mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees));

        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.UnitOfWork = _unitOfWork;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
          employeeVM.ImageNme= await DocumentSettings.UploadFile(employeeVM.Image, "Images");

            //Employee mappedEmployee = (Employee)employee;
            var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Employee>().Add(mappedEmp);

                var count = await _unitOfWork.Complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));


                }
            }
            return View(mappedEmp);
        }

        public async Task< IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee =  await _unitOfWork.Repository<Employee>().GetAsync(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();
            if(ViewName.Equals("Delete",StringComparison.OrdinalIgnoreCase))
            TempData["ImageName"] = employee.ImageName;

            return View(ViewName, mappedEmp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {

            if (id != employeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.Repository<Employee>().Update(mappedEmp);

                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the employee");

                return View(employeeVM);
            }
        }
        public async Task <IActionResult> Delete(int? id)
        {
            
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {
            try
            {

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                mappedEmp.ImageName = TempData["ImageName"] as string;
                _unitOfWork.Repository<Employee>().Delete(mappedEmp);

               var count= await _unitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSettings.Delete(employeeVM.ImageNme, "Images");
                    return RedirectToAction(nameof(Index));

                }
                return View(employeeVM);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, ("An error has occured during update the employee"));
                return View(employeeVM);
            }

        }
    }
}