using Dapper;
using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        public override List<Usuario> GetAll()
        {
            var usuarios = new List<Usuario>();

            Connection.Query<Usuario, Contato, EnderecoEntrega, Usuario>(
                $"SELECT * FROM {nameof(Usuario)} T " +
                $"LEFT JOIN {nameof(Contato)} C ON C.UsuarioId = T.Id " +
                $"LEFT JOIN {nameof(EnderecoEntrega)} EE ON EE.UsuarioId = T.Id;",
                (usuario, contato, enderecoEntrega) =>
                {
                    var usuarioMemory = usuarios.FirstOrDefault(x => x.Id == usuario.Id);

                    if (usuarioMemory == null)
                    {
                        usuario.Contato = contato;
                        usuario.AddEndereco(enderecoEntrega);
                        usuarios.Add(usuario);
                    }
                    else
                    {
                        usuarioMemory.AddEndereco(enderecoEntrega);
                    }                  

                    return usuario;
                });

            return usuarios;
        }

        public override Usuario Get(long id)
        {
            Usuario usuarioMemory = null;

            Connection.Query<Usuario, Contato, EnderecoEntrega, Usuario>(
                 $"SELECT * FROM {nameof(Usuario)} T " +
                 $"LEFT JOIN {nameof(Contato)} C ON C.UsuarioId = T.Id " +
                 $"LEFT JOIN {nameof(EnderecoEntrega)} EE ON EE.UsuarioId = T.Id " +
                 $"WHERE T.Id = @Id",
                 (usuario, contato, enderecoEntrega) =>
                 {
                     if (usuarioMemory == null)
                     {
                         usuario.Contato = contato;
                         usuario.AddEndereco(enderecoEntrega);
                         usuarioMemory = usuario;
                     }
                     else
                     {
                         usuarioMemory.AddEndereco(enderecoEntrega);
                     }                                                           

                     return usuario;
                 },
                 new { Id = id }
             );

            return usuarioMemory;
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

        public override void Update(Usuario entity)
        {
            Connection.Open();
            var transaction = Connection.BeginTransaction();

            try
            {
                Connection.Execute(UpdateSql<Usuario>(), entity, transaction);

                if (entity.Contato != null)
                    Connection.Execute(UpdateSql<Contato>(), entity.Contato, transaction);

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
