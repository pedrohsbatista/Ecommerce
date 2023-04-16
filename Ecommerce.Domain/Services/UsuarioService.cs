﻿using Ecommerce.Domain.Entities;
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
        
        public Usuario GetWithContato(long id)
        {
            return _usuarioRepository.GetWithContato(id);
        }
    }
}
