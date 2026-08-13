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
using ApplicationCore.Helpers;
using DataAccessLayer.Context;

namespace ApplicationCore.Services
{
    public class AuthRepository : IAuthRepository
    {
        private AppDBContext _dbcontext;
        private HashMiddleware _hashMiddleware;
        private JwtMiddleware _jwtMiddleware;
        public AuthRepository(AppDBContext dbcontext, HashMiddleware hashMiddleware, JwtMiddleware jwtMiddleware)
        {
            _dbcontext = dbcontext;
            _hashMiddleware = hashMiddleware;
            _jwtMiddleware = jwtMiddleware;
        }
        public async Task<TokenResultDto>Login(AuthDto auth)
        {
            var user = await _dbcontext.Users.Where(u => u.UserName == auth.UserName).Include(x => x.UserName).FirstOrDefaultAsync();

            if (user == null)
                return null;


            // check if password is correct
            if (!_hashMiddleware.VerifyPasswordHash(auth.Password, user.Password))
                return null;

            var token = _jwtMiddleware.CreateToken(user);

            return token;
        }
    }
}
