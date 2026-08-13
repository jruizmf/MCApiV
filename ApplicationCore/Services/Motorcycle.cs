using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using DataAccessLayer.Models.Dto;
using ApplicationCore.Middleware;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using DataAccessLayer.Models;
using ApplicationCore.Helpers;
using DataAccessLayer.Context;

namespace ApplicationCore.Services
{ 
    public class MotorcycleRepository : IMotorcycleRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public MotorcycleRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<Motorcycle>> GetAll()
        {
            var motorcycles = await _dbcontext.Motorcycles.Include(d => d.MotorcycleImages).ToListAsync();
            return motorcycles;
        }

    
        public async Task<Motorcycle> GetOne(Guid Id)
        {
            var motorcycle = await _dbcontext.Motorcycles.Include(d => d.MotorcycleImages).Include(d => d.User).SingleOrDefaultAsync(m => m.Id == Id);

            return motorcycle;
        }

        public async Task<string> Add(Motorcycle property)
        {
            try
            {
                _dbcontext.Motorcycles.Add(property);

                await _dbcontext.SaveChangesAsync();

                return "La propiedad agregado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> Update(Guid id, Motorcycle property)
        {
            var motorcycle = await _dbcontext.Motorcycles.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (motorcycle != null)
            {
                return "La propiedad ya existe";
            }

            motorcycle.Trademark = property.Trademark;
            motorcycle.Line = property.Line;
            motorcycle.Model = property.Model;
            motorcycle.Color = property.Color;
            motorcycle.Plate = property.Plate;
            motorcycle.SerialNumber = property.SerialNumber;
            motorcycle.UserId = property.UserId;
            motorcycle.DateAdded = DateTime.Now;


            await _dbcontext.SaveChangesAsync();
            return "La propiedad modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var motorcycle = _dbcontext.Motorcycles.Where(u => u.Id == id).FirstOrDefault();
            if (motorcycle == null)
            {
                return "El Articulo no existe";
            }
           
            _dbcontext.Motorcycles.Remove(motorcycle);
            await _dbcontext.SaveChangesAsync();

            return "Propiedad eliminada exitosamente";
        }
    }
}
