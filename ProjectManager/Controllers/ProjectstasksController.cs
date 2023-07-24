using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Context;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectstasksController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public ProjectstasksController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Projectstasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projectstask>>> GetProjectstasks()
        {
          if (_context.Projectstasks == null)
          {
              return NotFound();
          }
            return await _context.Projectstasks.ToListAsync();
        }

        // GET: api/Projectstasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projectstask>> GetProjectstask(int id)
        {
          if (_context.Projectstasks == null)
          {
              return NotFound();
          }
            var projectstask = await _context.Projectstasks.FindAsync(id);

            if (projectstask == null)
            {
                return NotFound();
            }

            return projectstask;
        }

        // PUT: api/Projectstasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectstask(int id, Projectstask projectstask)
        {
            if (id != projectstask.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectstask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectstaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(projectstask);
        }

        // POST: api/Projectstasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projectstask>> PostProjectstask(Projectstask projectstask)
        {
          if (_context.Projectstasks == null)
          {
              return Problem("Entity set 'ProjectDBContext.Projectstasks'  is null.");
          }
            _context.Projectstasks.Add(projectstask);
            await _context.SaveChangesAsync();

            return Ok(projectstask);
        }

        // DELETE: api/Projectstasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectstask(int id)
        {
            if (_context.Projectstasks == null)
            {
                return NotFound();
            }
            var projectstask = await _context.Projectstasks.FindAsync(id);
            if (projectstask == null)
            {
                return NotFound();
            }

            _context.Projectstasks.Remove(projectstask);
            await _context.SaveChangesAsync();

            return Ok(projectstask);
        }

        [HttpGet]
        [Route("listtasks/{moduleid}")]
        public async Task<ActionResult<IEnumerable<Projectstask>>> ListTask([FromRoute]int moduleid)
        {
            return Ok(_context.Projectstasks.Where(m => m.Moduleid == moduleid).ToList());
        }

         [HttpGet]
         [Route("mytasks/{empid}")]
         public async Task<ActionResult<IEnumerable<Projectstask>>> MyTask([FromRoute] int empid)
         {
             return Ok(_context.Projectstasks.Include(pt => pt.Project).Include(pt => pt.Module).Where(m => m.Employeeid == empid).ToList());
         }

        [HttpPut]
        [Route("updatestatus/{id}/{status}")]
        public async Task<ActionResult<IEnumerable<Projectstask>>> UpdateManager([FromRoute] int id, [FromRoute] string status)
        {
            Projectstask projectstask = await _context.Projectstasks.FindAsync(id);
            if (projectstask != null)
            {
                projectstask.Status = status;
                _context.Entry(projectstask).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(projectstask);
        }

        [HttpGet]
        [Route("reportlist")]
        public async Task<ActionResult<IEnumerable<Projectstask>>> ReportList()
        {
            var reportlist = (from pt in _context.Projectstasks
                              join e in _context.Employees on pt.Employeeid equals e.Id
                              join p in _context.Projects on pt.Projectid equals p.Id
                              join m in _context.Projectmodules on pt.Moduleid equals m.Id

                              select new
                              {
                                  pt,
                                  employeename = e.Name,
                                  projectname = p.Name,
                                  modulename = m.Name,
                                  projecttask = pt.Task,
                                  projectstatus = pt.Status,
                              }
                            ).ToList();

            return Ok(reportlist);
        }

        private bool ProjectstaskExists(int id)
        {
            return (_context.Projectstasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
