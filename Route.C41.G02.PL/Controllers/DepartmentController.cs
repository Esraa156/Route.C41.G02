using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.BLL.Interfaces;
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
            return View();
        }
    }
}
