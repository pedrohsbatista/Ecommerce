using System.Collections.Generic;

namespace Ecommerce.Domain.IRepository
{
    public interface IBaseRepository<T>
    {
        public List<T> GetAll();

        public T Get(long id);

        public void Insert(T entity);

        public void Update(T entity);

        public void Delete(long id);
    }
}
