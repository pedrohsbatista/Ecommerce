using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;

namespace Ecommerce.Domain.Services
{
    public class DepartamentoService : BaseService<Departamento>
    {        
        public DepartamentoService(IDepartamentoRepository departamentoRepository) : base(departamentoRepository)
        {
        }       
    }
}
