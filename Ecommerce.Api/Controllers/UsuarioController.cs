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

        [HttpGet("WithContato/{id}")]
        public IActionResult GetWithContato(long id)
        {
            try
            {
                var result = _usuarioService.GetWithContato(id);

                if (result == null) 
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
