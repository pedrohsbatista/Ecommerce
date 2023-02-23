using System.Collections.Generic;

namespace Ecommerce.Repository.Repositories
{
    public class BaseRepository<T>
    {      
        public virtual List<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public virtual T Get(long id)
        {
            throw new System.NotImplementedException();
        }
               
        public virtual void Insert(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Delete(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
