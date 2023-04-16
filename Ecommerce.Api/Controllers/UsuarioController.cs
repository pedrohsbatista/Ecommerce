using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : BaseController<Usuario>
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }
    }
}
