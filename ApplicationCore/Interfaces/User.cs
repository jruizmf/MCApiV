using DataAccessLayer.Models;
using DataAccessLayer.Models.Dto;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetOne(Guid Id);
        Task<string> Add(UserDto user);
        Task<string> Update(Guid Id, UserDto user);
        Task<string> Delete(Guid Id);
    }
}
