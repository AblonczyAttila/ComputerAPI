using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAPI.Models;

namespace ComputerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompController : ControllerBase
    {
        private readonly ComputerContext _context;

        public CompController(ComputerContext context)
        {
            _context = context;
        }

        // GET: api/Comp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comp>>> GetComps()
        {
            return await _context.Comps.Include(c => c.Os).ToListAsync();
        }

        // GET: api/Comp/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Comp>> GetComp(Guid id)
        {
            var comp = await _context.Comps.Include(c => c.Os).FirstOrDefaultAsync(c => c.Id == id);

            if (comp == null)
            {
                return NotFound();
            }

            return comp;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComp(Guid id, CreateCompDto createCompDto)
        {
            
            var existingComp = await _context.Comps.FindAsync(id);
            if (existingComp == null)
            {
                return NotFound();
            }

            
            _context.Entry(existingComp).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Comp
        [HttpPost]
        public async Task<ActionResult<Comp>> PostComp(CreateCompDto createCompDto)
        {
            Comp comp = new Comp
            {
                Id = Guid.NewGuid(),
                Brand = createCompDto.brand,
                Type = createCompDto.type,
                Display = createCompDto.display,
                Memory = createCompDto.memory,
                OsId = createCompDto.osid,
                CreatedTime = DateTime.Now
            };

            _context.Comps.Add(comp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComp", new { id = comp.Id }, comp);
        }

        // DELETE: api/Comp/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComp(Guid id)
        {
            var comp = await _context.Comps.FindAsync(id);
            if (comp == null)
            {
                return NotFound();
            }

            _context.Comps.Remove(comp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompExists(Guid id)
        {
            return _context.Comps.Any(e => e.Id == id);
        }
    }
}
