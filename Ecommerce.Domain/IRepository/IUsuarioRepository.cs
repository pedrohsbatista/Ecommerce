using Ecommerce.Domain.Storeds;
using Ecommerce.Domain.Entities;
using System.Collections.Generic;

namespace Ecommerce.Domain.IRepository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {        
        public Usuario GetContatoAndEndereco(long id);

        public List<StoredUsuario> StoredGetAll();

        public StoredUsuario StoredGet(long id);

        public void StoredInsert(StoredUsuario entity);

        public void StoredUpdate(StoredUsuario entity);

        public void StoredDelete(long id);
    }
}
