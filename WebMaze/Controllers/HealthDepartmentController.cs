using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebMaze.DbStuff.Repository;

namespace WebMaze.Controllers
{
    public class HealthDepartmentController : Controller
    {
        private HealthDepartmentRepository departmentRepository;
        private IMapper mapper;


        public HealthDepartmentController(HealthDepartmentRepository departmentRepository, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult HealthDepartment()
        {
            return View();
        }
    }
}
