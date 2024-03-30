//using Abp.Domain.Uow;
//using AutoMapper;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.Extensions.Hosting;
//using Route.C41.G02.BLL.Interfaces;
//using Route.C41.G02.BLL.Repositories;
//using Route.C41.G02.DAL.Models;
//using Route.C41.G02.PL.ViewModels;
//using System;
//using System.Collections.Generic;
//namespace Route.C41.G02.PL.Controllers
//{
//    //Inhertence
//    public class DepartmentController : Controller
//    {
//        //Association :-Composition
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IWebHostEnvironment _env;
//        private readonly IMapper mapper;


//        public DepartmentController(IUnitOfWork unitOfWork , IWebHostEnvironment env, IMapper mapperr) //Ask CLR for creating object from any class implementing interface IDepartmentRepository
//        {
//            _unitOfWork = unitOfWork;
//            _env = env;
//            this.mapper = mapper;
//        }
//        public IActionResult Index()
//        {
//            var departments = _unitOfWork.DepartmentRepository.GetAll();
//            var mappedDepartments = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

//            return View(departments);

//        }
//        [HttpGet]
//        public IActionResult Create()

//        {

//            return View();
//        }
//        [HttpPost]
//        public IActionResult Create(Department department)

//        {
//            var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
//            if (ModelState.IsValid) // server side validation
//            {
//                var count = _departmentRepository.Add(mappedDep);
//                _unitOfWork.DepartmentRepository.Add(mappedDep);
//                var count = _unitOfWork.Complete();
//                if (count > 0)
//                    return RedirectToAction(nameof(Index));
//            }

//            return View(department);
//        }

//        // Department/Details/id
//        // Department/Details

//        [HttpGet]
//        public IActionResult Details(int? id,string ViewName="Details")
//        {


//            if (id is null)
//            {
//                return BadRequest(); //400
//            }

//            var dept = _idepartmentRepository.GetById(id.Value);

//            if (dept is null)
//            {
//                return NotFound(); //404
//            }
//            return View(ViewName, dept);
//        }



//            // Department/Details/id
//            // Department/Details

//            [HttpGet]
//            [ValidateAntiForgeryToken]
//            public IActionResult Edit(int ? id)
//            {


//            //if (!id.HasValue)
//            //{
//            //    return BadRequest(); //400
//            //}

//            //var dept = _idepartmentRepository.GetById(id.Value);

//            //if (dept is null)
//            //{
//            //    return NotFound(); //404
//            //}
//            //return View(dept);


//            return Details( id, "Edit");
            
//            }


//        [HttpPost]
//        public IActionResult Edit([FromRoute] int id, Department department)
//        {
//            if (id != department.Id)
//            {
//                return BadRequest("An Error Ya Esraa !");
//            }
//            if (!ModelState.IsValid)
//            {
//                return View(department);
//            }

//            try
//            {

//                _idepartmentRepository.Update(department);
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception Ex)
//            {
//                //1. Log Exception
//                //2. Friendly message
//                if (_env.IsDevelopment())
//                {
//                    ModelState.AddModelError(string.Empty, Ex.Message);
//                }
//                else
//                {

//                    ModelState.AddModelError(string.Empty, "An Error Has Ocuured During updating The Department ");
//                }
//                return View(department);

//            }

//        }
//            [HttpGet]
//            public IActionResult Delete(int? id)
//            {
//                return Details(id, "Delete");   
//            }
//        [HttpPost]
//        public IActionResult Delete(Department department)
//        {
//            try
//            {
//                _idepartmentRepository.Delete(department);
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception Ex)
//            {
//                //1.Log Exception
//                //Show Friendly Message

//                if (_env.IsDevelopment())
//                {
//                    ModelState.AddModelError(string.Empty, Ex.Message);
//                }
//                else
//                {

//                    ModelState.AddModelError(string.Empty, "An Error Has Ocuured During updating The Department ");
//                }
//                return View(department);
//            }

//        }
//    }

//    }

