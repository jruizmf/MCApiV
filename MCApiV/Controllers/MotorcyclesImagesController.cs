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
    public class MotorcyclesImagesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public MotorcyclesImagesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/PropertyImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotorcycleImage>>> GetPropertyImages()
        {
          if (_context.MotorcycleImages == null)
          {
              return NotFound();
          }
            return await _context.MotorcycleImages.ToListAsync();
        }

        // GET: api/PropertyImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MotorcycleImage>> GetPropertyImage(Guid id)
        {
          if (_context.MotorcycleImages == null)
          {
              return NotFound();
          }
            var propertyImage = await _context.MotorcycleImages.FindAsync(id);

            if (propertyImage == null)
            {
                return NotFound();
            }

            return propertyImage;
        }

        // PUT: api/PropertyImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropertyImage(Guid id, MotorcycleImage propertyImage)
        {
            if (id != propertyImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(propertyImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyImageExists(id))
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

        // POST: api/PropertyImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MotorcycleImage>> PostPropertyImage(MotorcycleImage propertyImage)
        {
          if (_context.MotorcycleImages == null)
          {
              return Problem("Entity set 'AppDBContext.PropertyImages'  is null.");
          }
            _context.MotorcycleImages.Add(propertyImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPropertyImage", new { id = propertyImage.Id }, propertyImage);
        }

        // DELETE: api/PropertyImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyImage(Guid id)
        {
            if (_context.MotorcycleImages == null)
            {
                return NotFound();
            }
            var propertyImage = await _context.MotorcycleImages.FindAsync(id);
            if (propertyImage == null)
            {
                return NotFound();
            }

            _context.MotorcycleImages.Remove(propertyImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyImageExists(Guid id)
        {
            return (_context.MotorcycleImages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
