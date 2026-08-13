using DataAccessLayer.Models;
using DataAccessLayer.Models.Dto;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<List<Motorcycle>> GetAll();
        Task<Motorcycle> GetOne(Guid Id);
        Task<string> Add(Motorcycle Tienda);
        Task<string> Update(Guid Id, Motorcycle Tienda);
        Task<string> Delete(Guid Id);
    }
}
