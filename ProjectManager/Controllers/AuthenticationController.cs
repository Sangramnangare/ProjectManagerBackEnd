using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Context;
using ProjectManager.DTOs;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        ProjectDBContext _context;
        public AuthenticationController(ProjectDBContext projectDBContext) 
        {
            this._context = projectDBContext;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<IEnumerable<Employee>>> Login([FromBody]EmployeeDTO employee)
        {
            IList<Employee>employees = _context.Employees.Where(e=>e.Email == employee.Email && e.Password == employee.Password).ToList();
            return Ok(employees);
        }
    }
}
