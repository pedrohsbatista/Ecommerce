using Ecommerce.Domain.Storeds;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using System.Collections.Generic;

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

        public StoredUsuario StoredGet(long id)
        {
            return _usuarioRepository.StoredGet(id);
        }

        public List<StoredUsuario> StoredGetAll()
        {
            return _usuarioRepository.StoredGetAll();
        }

        public void StoredInsert(StoredUsuario entity)
        {
            _usuarioRepository.StoredInsert(entity);
        }

        public void StoredUpdate(StoredUsuario entity)
        {
            _usuarioRepository.StoredUpdate(entity);
        }

        public void StoredDelete(long id)
        {
            _usuarioRepository.StoredDelete(id);
        }
    }
}
