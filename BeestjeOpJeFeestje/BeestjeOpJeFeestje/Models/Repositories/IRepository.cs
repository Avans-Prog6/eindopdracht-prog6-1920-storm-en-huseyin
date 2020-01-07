using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Get(int? ID);
        Task<List<T>> GetAll();
        Task Create(T type);
        Task Update(T type);
        Task Delete(T type);
        bool Exists(int? ID);
    }
}