using Ecommerce.Domain.IRepository;
using System.Collections.Generic;

namespace Ecommerce.Domain.Services
{
    public class BaseService<T>
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual T Get(long id)
        {
           return _repository.Get(id);
        }

        public virtual List<T> GetAll()
        {
           return _repository.GetAll();
        }

        public virtual void Insert(T entity)
        {
            _repository.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            _repository.Update(entity);
        }

        public virtual void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
