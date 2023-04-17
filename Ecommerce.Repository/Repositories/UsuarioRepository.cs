using Dapper;
using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Ecommerce.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {        
        public UsuarioRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {            
        }

        public override Usuario Get(long id)
        {
           return Connection.Query<Usuario, Contato, Usuario>(
                $"SELECT * FROM {nameof(Usuario)} T " +
                $"LEFT JOIN {nameof(Contato)} C ON C.UsuarioId = T.Id " +
                $"WHERE T.Id = @Id",
                (usuario, contato) =>
                {
                    usuario.Contato = contato;
                    return usuario;
                },
                new { Id = id}
            ).SingleOrDefault();
        }

        public override void Insert(Usuario entity)
        {
            Connection.Open();
            var transaction = Connection.BeginTransaction();

            try
            {                        
                entity.Id = Connection.Query<long>(InsertSql<Usuario>(), entity, transaction).Single();
                
                if (entity.Contato != null)
                {
                    entity.Contato.UsuarioId = entity.Id;                    
                    entity.Contato.Id = Connection.Query<long>(InsertSql<Contato>(), entity.Contato, transaction).Single();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                Connection.Close();
            }          
        }
    }
}
