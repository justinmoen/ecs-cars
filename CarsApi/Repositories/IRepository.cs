using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsApi.Repositories
{
    public interface IRepository<T> 
    {
        public T Create(T entity);
        public Task<IEnumerable<T>> GetAll();
        public T GetById(int id);
        public T Update(int id);
        public T Delete(int id);
    }
}
