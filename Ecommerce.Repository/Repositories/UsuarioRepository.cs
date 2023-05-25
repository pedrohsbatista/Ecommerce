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

            Connection.Query<Usuario, Contato, EnderecoEntrega, UsuarioDepartamento, Departamento, Usuario>(
                $"SELECT * FROM {nameof(Usuario)} T " +
                $"LEFT JOIN {nameof(Contato)} C ON C.UsuarioId = T.Id " +
                $"LEFT JOIN {nameof(EnderecoEntrega)} EE ON EE.UsuarioId = T.Id " +
                $"LEFT JOIN {nameof(UsuarioDepartamento)} UD ON UD.UsuarioId = T.Id " +
                $"LEFT JOIN {nameof(Departamento)} D ON D.Id = UD.DepartamentoId;",
                (usuario, contato, enderecoEntrega, usuarioDepartamento, departamento) =>
                {
                    var usuarioMemory = usuarios.FirstOrDefault(x => x.Id == usuario.Id);

                    if (usuarioMemory == null)
                    {
                        usuario.Contato = contato;
                        usuario.AddEndereco(enderecoEntrega);                        
                        usuario.AddDepartamento(usuarioDepartamento, departamento);                        
                        usuarios.Add(usuario);
                    }
                    else
                    {
                        if (!usuarioMemory.Enderecos.Any(x => x.Id == enderecoEntrega.Id))
                            usuarioMemory.AddEndereco(enderecoEntrega);

                        if (!usuarioMemory.Departamentos.Any(x => x.Id == usuarioDepartamento.Id))                                                    
                            usuarioMemory.AddDepartamento(usuarioDepartamento, departamento);                        
                    }                  

                    return usuario;
                });

            return usuarios;
        }

        public override Usuario Get(long id)
        {
            Usuario usuarioMemory = null;

            Connection.Query<Usuario, Contato, EnderecoEntrega, UsuarioDepartamento, Departamento, Usuario>(
                 $"SELECT * FROM {nameof(Usuario)} T " +
                 $"LEFT JOIN {nameof(Contato)} C ON C.UsuarioId = T.Id " +
                 $"LEFT JOIN {nameof(EnderecoEntrega)} EE ON EE.UsuarioId = T.Id " +
                 $"LEFT JOIN {nameof(UsuarioDepartamento)} UD ON UD.UsuarioId = T.Id " +
                 $"LEFT JOIN {nameof(Departamento)} D ON D.Id = UD.DepartamentoId " +
                 $"WHERE T.Id = @Id;",
                 (usuario, contato, enderecoEntrega, usuarioDepartamento, departamento) =>
                 {
                     if (usuarioMemory == null)
                     {
                         usuario.Contato = contato;
                         usuario.AddEndereco(enderecoEntrega);                         
                         usuario.AddDepartamento(usuarioDepartamento, departamento);
                         usuarioMemory = usuario;
                     }
                     else
                     {
                         if (!usuarioMemory.Enderecos.Any(x => x.Id == enderecoEntrega.Id))
                            usuarioMemory.AddEndereco(enderecoEntrega);

                         if (!usuarioMemory.Departamentos.Any(x => x.Id == usuarioDepartamento.Id))                                                     
                             usuarioMemory.AddDepartamento(usuarioDepartamento, departamento);                         
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

                foreach (var enderecoEntrega in entity.Enderecos)
                {
                    enderecoEntrega.UsuarioId = entity.Id;
                    enderecoEntrega.Id = Connection.Query<long>(InsertSql<EnderecoEntrega>(), enderecoEntrega, transaction).Single();
                }

                foreach (var usuarioDepartamento in entity.Departamentos)
                {
                    usuarioDepartamento.UsuarioId = entity.Id;
                    usuarioDepartamento.DepartamentoId = usuarioDepartamento.Departamento.Id;
                    usuarioDepartamento.Id = Connection.Query<long>(InsertSql<UsuarioDepartamento>(), usuarioDepartamento, transaction).Single();
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
                {
                    entity.Contato.UsuarioId = entity.Id;

                    if (entity.Contato.Id == 0)                                            
                        entity.Contato.Id = Connection.Query<long>(InsertSql<Contato>(), entity.Contato, transaction).Single();                    
                    else                    
                        Connection.Execute(UpdateSql<Contato>(), entity.Contato, transaction);                                      
                }
                else
                {
                    var contatoDb = Connection.QuerySingleOrDefault<Contato>($"SELECT * FROM {nameof(Contato)} T WHERE T.UsuarioId = @UsuarioId", new { UsuarioId = entity.Id }, transaction);
                    
                    if (contatoDb != null)
                        Connection.Execute(DeleteSql<Contato>(), new { Id = contatoDb.Id },  transaction);
                }

                var enderecosDb = Connection.Query<EnderecoEntrega>($"SELECT * FROM {nameof(EnderecoEntrega)} T WHERE T.UsuarioId = @UsuarioId", new { UsuarioId = entity.Id }, transaction).ToList();

                foreach (var enderecoEntrega in entity.Enderecos.Where(x => x.Id == 0))
                {
                    enderecoEntrega.UsuarioId = entity.Id;
                    enderecoEntrega.Id = Connection.Query<long>(InsertSql<EnderecoEntrega>(), enderecoEntrega, transaction).Single();
                }

                foreach (var enderecoEntregaDb in enderecosDb.Where(x => !entity.Enderecos.Any(y => y.Id == x.Id)))
                {
                    Connection.Execute(DeleteSql<EnderecoEntrega>(), new { Id = enderecoEntregaDb.Id }, transaction);
                }

                foreach (var enderecoEntregaDb in enderecosDb.Where(x => entity.Enderecos.Any(y => y.Id == x.Id)))
                {
                    var enderecoEntregaMemory = entity.Enderecos.First(x => x.Id == enderecoEntregaDb.Id);
                    enderecoEntregaMemory.UsuarioId = entity.Id;
                    Connection.Execute(UpdateSql<EnderecoEntrega>(), enderecoEntregaMemory, transaction);
                }

                var departamentosDb = Connection.Query<UsuarioDepartamento>($"SELECT * FROM {nameof(UsuarioDepartamento)} T WHERE T.UsuarioId = @UsuarioId", new { UsuarioId = entity.Id }, transaction).ToList();

                foreach (var usuarioDepartamento in entity.Departamentos.Where(x => x.Id == 0))
                {
                    usuarioDepartamento.UsuarioId = entity.Id;
                    usuarioDepartamento.DepartamentoId = usuarioDepartamento.Departamento.Id;
                    usuarioDepartamento.Id = Connection.Query<long>(InsertSql<UsuarioDepartamento>(), usuarioDepartamento, transaction).Single();
                }

                foreach (var usuarioDepartamentoDb in departamentosDb.Where(x => !entity.Departamentos.Any(y => y.Id == x.Id)))
                {
                    Connection.Execute(DeleteSql<UsuarioDepartamento>(), new { Id = usuarioDepartamentoDb.Id }, transaction);
                }

                foreach (var usuarioDepartamentoDb in departamentosDb.Where(x => entity.Departamentos.Any(y => y.Id == x.Id)))
                {
                    var usuarioDepartamentoMemory = entity.Departamentos.First(x => x.Id == usuarioDepartamentoDb.Id);
                    usuarioDepartamentoMemory.UsuarioId = entity.Id;
                    usuarioDepartamentoMemory.DepartamentoId = usuarioDepartamentoMemory.Departamento.Id;
                    Connection.Execute(UpdateSql<UsuarioDepartamento>(), usuarioDepartamentoMemory, transaction);
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
