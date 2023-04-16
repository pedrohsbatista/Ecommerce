using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.IRepository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        public Usuario GetWithContato(long id);
    }
}
