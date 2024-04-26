using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Route.C41.G02.PL.Controllers
{
	//Inhertence

	//Inheritance : DepartmentController is a Controller
	//Composition : DepartmentController has a Department Repository
	[Authorize]

	public class DepartmentController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IWebHostEnvironment _env;
            private readonly IMapper mapper;

            public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _env = env;
                this.mapper = mapper;
            }
            public async Task<IActionResult> Index()
            {
                var departments = await _unitOfWork.Repository<Department>().GetAllAsync();
                var mappedDepartments =  mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
                return View(mappedDepartments);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
            {
                var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                if (ModelState.IsValid) // server side validation
                {
                     _unitOfWork.Repository<Department>().Add(mappedDep);
                    var count = await _unitOfWork.Complete();
                    if (count > 0)
                        return RedirectToAction(nameof(Index));
                }
                return View(mappedDep);
            }

            // /Department/Details/10 
            // /Department/Details 

            [HttpGet]
            public async Task<IActionResult> Details(int? id, string viewName = "Details")
            {
                if (!id.HasValue)//id is null
                    return BadRequest();

                var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);
                _unitOfWork.Complete();
                if (department is null)
                    return NotFound();
                var mappedDep = mapper.Map<Department, DepartmentViewModel>(department);
                return View(viewName, mappedDep);
            }

            [HttpGet]
            public async Task<IActionResult> Edit(int? id)
            {
                return await Details(id, "Edit");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
            {
                if (id != departmentVM.Id)
                {
                    return BadRequest(new ViewResult());
                }
                if (!ModelState.IsValid)
                {
                    return View(departmentVM);
                }
                try
                {
                    var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.Repository<Department>().Update(mappedDep);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //log exception
                    if (_env.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        ModelState.AddModelError(string.Empty, ("An error has occured during updating the department"));
                    return View(departmentVM);
                }
            }
            public async Task<IActionResult> Delete(int? id)
            {
                return await Details(id, "Delete");
            }

            [HttpPost]
            public IActionResult Delete(DepartmentViewModel departmentVM)
            {
                try
                {
                    var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.Repository<Department>().Delete(mappedDep);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        ModelState.AddModelError(string.Empty, ("An error has occured during updating the department"));
                    return View(departmentVM);
                    //return View("Error", new ErrorViewModel());
                }
            }

            ///Delete using modal
            ///[HttpPost]
            ///[ValidateAntiForgeryToken]
            ///public IActionResult Delete(int id)
            ///{
            ///	var departmentToDelete = _departmentRepo.Get(id);
            ///	if (departmentToDelete == null)
            ///	{
            ///		return NotFound(); 
            ///	}
            ///	_departmentRepo.Delete(departmentToDelete); 
            ///	return RedirectToAction("Index");
            ///}

        }
    }

