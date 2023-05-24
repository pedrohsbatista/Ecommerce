using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : BaseController<Departamento>
    {       
        public DepartamentoController(DepartamentoService departamentoService) : base(departamentoService)
        {     
        }
    }
}
