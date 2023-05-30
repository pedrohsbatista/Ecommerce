using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Microsoft.Extensions.Options;

namespace Ecommerce.RepositoryContrib.Repositories
{
    public class DepartamentoRepositoryContrib : BaseRepositoryContrib<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepositoryContrib(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }
    }
}
