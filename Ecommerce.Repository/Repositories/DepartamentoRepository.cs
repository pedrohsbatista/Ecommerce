using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Microsoft.Extensions.Options;

namespace Ecommerce.Repository.Repositories
{
    public class DepartamentoRepository : BaseRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }
    }
}
