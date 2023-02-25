using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private static List<Usuario> _usuarios = new List<Usuario>();

        public override List<Usuario> GetAll()
        {
            return _usuarios;
        }

        public override Usuario Get(long id)
        {
            return _usuarios.FirstOrDefault(x => x.Id == id);
        }

        public override void Insert(Usuario entity)
        {
            entity.Id = _usuarios.Count + 1;
            _usuarios.Add(entity);            
        }

        public override void Update(Usuario entity)
        {
            var usuario = _usuarios.FirstOrDefault(x => x.Id == entity.Id);

            usuario = entity;
        }

        public override void Delete(long id)
        {
            var usuario = Get(id);

            _usuarios.Remove(usuario);
        }
    }
}
