using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using DataAccessLayer.Models.Dto;

namespace MCApiV.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResultDto>> Post(AuthDto auth)
        {
            try
            {
                var result = await _authRepository.Login(auth);

                return Ok(new { User = result, Token = result.Token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
