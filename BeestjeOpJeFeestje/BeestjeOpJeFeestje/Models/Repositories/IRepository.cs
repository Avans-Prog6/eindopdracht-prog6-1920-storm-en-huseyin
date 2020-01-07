using System.Collections.Generic;

namespace BeestjeOpJeFeestje.Models.Repositories
{
    public interface IRepository<T>
    {
        T Get();
        List<T> GetAll();
        void Create(T type);
        void Update(T type);
        void Delete(T type);
    }
}