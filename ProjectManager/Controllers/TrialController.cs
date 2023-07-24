using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Context;
using ProjectManager.DTOs;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrialController : ControllerBase
    {
        ProjectDBContext _context;
        public TrialController(ProjectDBContext Context) 
        {
            _context = Context; 
        }

        [HttpPost]
        [Route("addproject")]
        public async Task<ActionResult<IEnumerable<Project>>> List([FromBody]Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Ok(project); 
        }

        [HttpGet]
        [Route("listproject")]

        public async Task<ActionResult<IEnumerable<Project>>> listproject()
        {
            return Ok(_context.Projects.Include(p => p.Manager).ToList());
        }

        [HttpPost]
        [Route("emailpassword")]
        public async Task<ActionResult<IEnumerable<Employee>>> EmailPassword([FromBody]EmployeeDTO employee)
        {
           return Ok( _context.Employees.Where(e => e.Email == employee.Email && e.Password == employee.Password));
        }

        [HttpPost]
        [Route("addtask")]
        public async Task<ActionResult<IEnumerable<Projectstask>>> AddTask([FromBody]Projectstask task)
        {
            _context.Projectstasks.Add(task); 
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpGet]
        [Route("listtasks")]
        public async Task<ActionResult<IEnumerable<Project>>> ListTasks()
        {
            return Ok(_context.Projects.Include(p => p.Projectstasks).ToList());
        }

        [HttpPut]
        [Route("updatemanager/{id}/{managerid}")]
        public async Task<ActionResult<IEnumerable<Project>>> UpdateManager([FromRoute]int id, [FromRoute]int managerid)
        {
            Project project =  await _context.Projects.FindAsync(id);
            if(project != null)
            {
                project.Managerid = managerid;
                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(project);
        }

        [HttpGet]
        [Route("taskbypid/{id}")]
        public async Task<ActionResult<IEnumerable<Projectstask>>> Taskbypid([FromRoute]int id)
        {
            Project project = await _context.Projects.FindAsync(id);
            if(project != null)
            {
                _context.Projectstasks.Include(p => p.Project).Where(p => p.Projectid == id).ToList();
            }
            return Ok(project);
        }

        [HttpGet]
        [Route("taskbyeid/{id}")]
        public async Task<ActionResult<IEnumerable<Projectstask>>> Taskbyeid([FromRoute] int id)
        {
            Project project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projectstasks.Include(p => p.Project).Where(p => p.Employeeid == id).ToList();
            }
            return Ok(project);
        }

        [HttpDelete]
        [Route("deletetask/{id}")]
        public async Task<ActionResult<IEnumerable<Projectstask>>> DeleteTask([FromRoute]int id)
        {
            Projectstask projectstask = await _context.Projectstasks.FindAsync(id);
            if(projectstask != null)
            {
                _context.Projectstasks.Remove(projectstask);
                _context.SaveChangesAsync();
            }
            return Ok(projectstask);
        }

        [HttpDelete]
        [Route("deleteproject/{id}")]
        public async Task<ActionResult<IEnumerable<Project>>> DeleteProject([FromRoute]int id)
        {
            List<Projectstask> projectstasks = _context.Projectstasks.Where(t => t.Projectid == id).ToList();
            List<Projectmodule> projectmodules = _context.Projectmodules.Where(t => t.Projectid == id).ToList();
            foreach(Projectstask projectstask in projectstasks)
            {
                _context.Projectstasks.Remove(projectstask);
            }
            foreach (Projectmodule projectmodule in projectmodules)
            {
                _context.Projectmodules.Remove(projectmodule);
            }
            Project project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            _context.SaveChangesAsync();
            return Ok(project);
        }
    }
}
