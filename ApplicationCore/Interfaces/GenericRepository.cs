using ApplicationCore.Models.Dto;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IGenericyRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetOne(Guid Id);
        Task<string> Add(T Tienda);
        Task<string> Update(Guid Id, T Tienda);
        Task<string> Delete(Guid Id);
    }
}
