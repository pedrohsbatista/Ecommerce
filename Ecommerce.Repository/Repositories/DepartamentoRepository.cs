using Dapper;
using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository.Repositories
{
    public class DepartamentoRepository : BaseRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }

        public override List<Departamento> GetAll()
        {
            return Connection.Query<Departamento>("SELECT Id, Nome as Descricao FROM DEPARTAMENTO T").ToList();
        }
    }
}
