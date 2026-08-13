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
using ApplicationCore;
using DataAccessLayer.Models;
using ApplicationCore.Helpers;
using DataAccessLayer.Context;

namespace ApplicationCore.Services
{

    public class UserRepository : IUserRepository
    {
        public readonly AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        public UserRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _dbcontext.Users.ToListAsync<User>();
            return users;
        }
        public async Task<User> GetOne(Guid Id)
        {
            var user = await _dbcontext.Users.SingleOrDefaultAsync(u => u.Id == Id);
            return user;
        }
        public async Task<string> Add(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new AppException("la contraseña es requerida");

            if (_dbcontext.Users.Any(x => x.UserName == userDto.UserName))
                throw new AppException("El usuario: \"" + userDto.UserName + "\" ya ha sido utilizado");

            byte[] passwordHash, passwordSalt;

            _hashMiddleware.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

            User user = new User() { 

            };
            user.UserName = userDto.UserName;
            user.Status = 1;
            user.Password = passwordHash;

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();


            return user.Id.ToString();
        }
        public async Task<string> Update(Guid id, UserDto userDto)
        {
            var user = await _dbcontext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
                throw new AppException("El usuario no existe");


            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userDto.UserName) && user.UserName != userDto.UserName)
            {
                userDto.UserName = user.UserName;
            }

            // update password if provided
            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                _hashMiddleware.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                user.Password = passwordHash;
            }

            _dbcontext.Users.Update(user);
            _dbcontext.SaveChanges();
            await _dbcontext.SaveChangesAsync();
            return "Usuario modificado exitosamente";
        }
        public async Task<string> Delete(Guid id)
        {
            var user = _dbcontext.Users.SingleOrDefault(empid => empid.Id == id);
            if (user == null)
            {
                return "El usuario no existe";
            }
            _dbcontext.Users.Remove(user);

            await _dbcontext.SaveChangesAsync();

            return "Usuario eliminado exitosamente";
        }
    }
}
