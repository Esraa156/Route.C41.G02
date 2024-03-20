using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
namespace Route.C41.G02.PL.Controllers
{
    //Inhertence
    public class DepartmentController : Controller
    {
        //Association :-Composition
        private readonly IDepartmentRepository _idepartmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository) //Ask CLR for creating object from any class implementing interface IDepartmentRepository
        {
            _idepartmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _idepartmentRepository.GetAll();
            return View(departments);

        }
        public IActionResult Create()

        {

            return View();
        }
    }
}
