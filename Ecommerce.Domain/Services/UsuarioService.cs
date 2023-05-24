using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;

namespace Ecommerce.Domain.Services
{
    public class UsuarioService : BaseService<Usuario>
    {        
        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {            
        }       
    }
}
