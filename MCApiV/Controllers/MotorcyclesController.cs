using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models.Dto;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace MCApiV.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public MotorcyclesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Properties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorcycle>>> GetProperties()
        {
          if (_context.Motorcycles == null)
          {
              return NotFound();
          }
            return await _context.Motorcycles.ToListAsync();
        }

        // GET: api/Properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorcycle>> GetProperty(Guid id)
        {
          if (_context.Motorcycles == null)
          {
              return NotFound();
          }
            var data = await _context.Motorcycles.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        // PUT: api/Properties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(Guid id, Motorcycle @property)
        {
            if (id != @property.Id)
            {
                return BadRequest();
            }

            _context.Entry(@property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Properties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataAccessLayer.Models.Motorcycle>> PostProperty(DataAccessLayer.Models.Motorcycle @property)
        {
          if (_context.Motorcycles == null)
          {
              return Problem("Entity set 'AppDBContext.Properties'  is null.");
          }
            _context.Motorcycles.Add(@property);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProperty", new { id = @property.Id }, @property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            if (_context.Motorcycles == null)
            {
                return NotFound();
            }
            var @property = await _context.Motorcycles.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }

            _context.Motorcycles.Remove(@property);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyExists(Guid id)
        {
            return (_context.Motorcycles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
