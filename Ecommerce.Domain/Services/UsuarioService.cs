using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;

namespace Ecommerce.Domain.Services
{
    public class UsuarioService : BaseService<Usuario>
    {        
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }       

        public Usuario GetContatoAndEndereco(long id)
        {
            return _usuarioRepository.GetContatoAndEndereco(id);
        }
    }
}
