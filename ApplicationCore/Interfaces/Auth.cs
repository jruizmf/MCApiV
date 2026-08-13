using DataAccessLayer.Models.Dto;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAuthRepository
    {
        Task<TokenResultDto> Login(AuthDto auth);
    }
}
