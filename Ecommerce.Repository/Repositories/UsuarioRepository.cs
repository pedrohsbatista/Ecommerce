using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private static List<Usuario> _usuarios = new List<Usuario>();

        public UsuarioRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {            
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
            var index = _usuarios.FindIndex(x => x.Id == entity.Id);
            _usuarios[index] = entity;            
        }

        public override void Delete(long id)
        {
            var usuario = Get(id);

            _usuarios.Remove(usuario);
        }
    }
}
