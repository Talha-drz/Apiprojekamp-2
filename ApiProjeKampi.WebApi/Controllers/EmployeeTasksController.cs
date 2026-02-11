using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly ApiContext _context;

        public EmployeeTasksController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult EmployeeTaskList()
        {
            var list = _context.EmployeeTasks.ToList();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Add(EmployeeTask);
            _context.SaveChanges();
            return Ok("Ekleme İşlei Başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteEmployeeTask(int id)
        {
            var value = _context.EmployeeTasks.Find(id);
            _context.EmployeeTasks.Remove(value);
            _context.SaveChanges();
            return Ok("Silme İşlei Başarılı");
        }
        [HttpGet("GetEmployeeTask")]
        public IActionResult GetEmployeeTask(int id)
        {
            var value = _context.EmployeeTasks.Find(id);
            return Ok(value);
        }
        [HttpPut]
        public IActionResult UpdateEmployeeTask(EmployeeTask EmployeeTask)
        {
            _context.EmployeeTasks.Update(EmployeeTask);
            _context.SaveChanges();
            return Ok("Güncelleme İşlei Başarılı");
        }
    }
}
