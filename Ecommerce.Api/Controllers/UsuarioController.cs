using Ecommerce.Domain.Storeds;
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

        [HttpGet("ContatoAndEndereco/{id}")]
        public IActionResult GetContatoAndEndereco(long id)
        {
            try
            {
                var result = _usuarioService.GetContatoAndEndereco(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Stored")]
        public IActionResult StoredGetAll()
        {
            try
            {
                var result = _usuarioService.StoredGetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Stored/{id}")]
        public IActionResult StoredGet(long id)
        {
            try
            {
                var result = _usuarioService.StoredGet(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Stored")]
        public IActionResult StoredPost(StoredUsuario entity)
        {
            try
            {
                _usuarioService.StoredInsert(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Stored")]
        public IActionResult StoredPut(StoredUsuario entity)
        {
            try
            {
                var entityId = entity?.Id ?? 0; 
                var result = _usuarioService.StoredGet(entityId);

                if (result == null)
                    return NotFound();

                _usuarioService.StoredUpdate(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("Stored/{id}")]
        public IActionResult StoredDelete(long id)
        {
            try
            {
                var result = _usuarioService.StoredGet(id);

                if (result == null)
                    return NotFound();

                _usuarioService.StoredDelete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
