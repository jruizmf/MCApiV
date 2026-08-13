using DataAccessLayer.Models;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfile>> GetAll();
        Task<UserProfile> GetOne(Guid Id);
        Task<Guid> Add(UserProfile Profile);
        Task<string> Update(Guid Id, UserProfile Profile);
        Task<string> Delete(Guid Id);
    }
}
