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
    public class ProjectmodulesController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public ProjectmodulesController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Projectmodules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projectmodule>>> GetProjectmodules()
        {
          if (_context.Projectmodules == null)
          {
              return NotFound();
          }
            return await _context.Projectmodules.ToListAsync();
        }

        // GET: api/Projectmodules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projectmodule>> GetProjectmodule(int id)
        {
          if (_context.Projectmodules == null)
          {
              return NotFound();
          }
            var projectmodule = await _context.Projectmodules.FindAsync(id);

            if (projectmodule == null)
            {
                return NotFound();
            }

            return projectmodule;
        }

        // PUT: api/Projectmodules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectmodule(int id, Projectmodule projectmodule)
        {
            if (id != projectmodule.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectmodule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectmoduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(projectmodule);
        }

        // POST: api/Projectmodules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projectmodule>> PostProjectmodule(Projectmodule projectmodule)
        {
          if (_context.Projectmodules == null)
          {
              return Problem("Entity set 'ProjectDBContext.Projectmodules'  is null.");
          }
            _context.Projectmodules.Add(projectmodule);
            await _context.SaveChangesAsync();

            return Ok(projectmodule);
        }

        // DELETE: api/Projectmodules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectmodule(int id)
        {
            if (_context.Projectmodules == null)
            {
                return NotFound();
            }
            var projectmodule = await _context.Projectmodules.FindAsync(id);
            if (projectmodule == null)
            {
                return NotFound();
            }

            _context.Projectmodules.Remove(projectmodule);
            await _context.SaveChangesAsync();

            return Ok(projectmodule);
        }

        [HttpGet]
        [Route("listmodules")]
        public async Task<ActionResult<IEnumerable<Projectmodule>>> ListModules()
        {
            return Ok(_context.Projectmodules.Include(p => p.Project).ToList());    
        }

        [HttpGet]
        [Route("listmodulesbyid/{projectid}")]
        public async Task<ActionResult<IEnumerable<Projectmodule>>> ListModulesById([FromRoute]int projectid)
        {
            return Ok(_context.Projectmodules.Where(p => p.Projectid == projectid).ToList());
        }
            
        private bool ProjectmoduleExists(int id)
        {
            return (_context.Projectmodules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
