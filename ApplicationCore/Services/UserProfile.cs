using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using DataAccessLayer.Models.Dto;
using ApplicationCore.Middleware;
using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using ApplicationCore.Helpers;
using DataAccessLayer.Context;

namespace ApplicationCore.Services
{

    public class UserProfileRepository : IUserProfileRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public UserProfileRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<UserProfile>> GetAll()
        {
            var profiles = await _dbcontext.UserProfiles.Include(d => d.User).ToListAsync<UserProfile>();
            return profiles;
        }
        public async Task<UserProfile>GetOne(Guid Id)
        {
            var profile = await _dbcontext.UserProfiles.Include(d => d.User).SingleOrDefaultAsync(u => u.Id == Id);
            return profile;
        }

        public async Task<Guid> Add(UserProfile userProfile)
        {
           
            _dbcontext.UserProfiles.Add(userProfile);
            await _dbcontext.SaveChangesAsync();

            return userProfile.Id;
        }
        public async Task<string> Update(Guid id, UserProfile userProfile)
        {
            var profile = await _dbcontext.UserProfiles.SingleOrDefaultAsync(u => u.Id == id);
            if (profile == null)
                throw new AppException("Usuario no encontrado");

            profile.AddressNumber = userProfile.AddressNumber;
            profile.AddressStreet = userProfile.AddressStreet;
            profile.AddressNeighborhood = userProfile.AddressNeighborhood;
            profile.ZIP = userProfile.ZIP;
            profile.CityId = userProfile.CityId;
            profile.StateId = userProfile.StateId;
            profile.CountryId = userProfile.CountryId;
            profile.Latitude = userProfile.Latitude;
            profile.MunicipalityId = userProfile.MunicipalityId;
            profile.Longitude = userProfile.Longitude;

            await _dbcontext.SaveChangesAsync();
            return "Perfil modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var _userProfiles = _dbcontext.UserProfiles.SingleOrDefault(u => u.Id == id);
            if (_userProfiles == null)
            {
                return "El usuario no existe";
            }
            _dbcontext.UserProfiles.Remove(_userProfiles);

            await _dbcontext.SaveChangesAsync();

            return "Usuario eliminado exitosamente";
        }
    }
}
