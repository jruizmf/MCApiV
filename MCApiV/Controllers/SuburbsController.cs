using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace MCApiV.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuburbsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SuburbsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Suburbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suburbs>>> GetSuburbs()
        {
          if (_context.Suburbs == null)
          {
              return NotFound();
          }
            return await _context.Suburbs.ToListAsync();
        }

        // GET: api/Suburbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suburbs>> GetSuburbs(Guid id)
        {
          if (_context.Suburbs == null)
          {
              return NotFound();
          }
            var suburbs = await _context.Suburbs.FindAsync(id);

            if (suburbs == null)
            {
                return NotFound();
            }

            return suburbs;
        }

        // PUT: api/Suburbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuburbs(Guid id, Suburbs suburbs)
        {
            if (id != suburbs.Id)
            {
                return BadRequest();
            }

            _context.Entry(suburbs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuburbsExists(id))
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

        // POST: api/Suburbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suburbs>> PostSuburbs(Suburbs suburbs)
        {
          if (_context.Suburbs == null)
          {
              return Problem("Entity set 'AppDBContext.Suburbs'  is null.");
          }
            _context.Suburbs.Add(suburbs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuburbs", new { id = suburbs.Id }, suburbs);
        }

        // DELETE: api/Suburbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuburbs(Guid id)
        {
            if (_context.Suburbs == null)
            {
                return NotFound();
            }
            var suburbs = await _context.Suburbs.FindAsync(id);
            if (suburbs == null)
            {
                return NotFound();
            }

            _context.Suburbs.Remove(suburbs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuburbsExists(Guid id)
        {
            return (_context.Suburbs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
