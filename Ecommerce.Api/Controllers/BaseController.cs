using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    public class BaseController<T> : ControllerBase
    {
        private readonly BaseService<T> _service;

        public BaseController(BaseService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {
            try
            {
                var result = _service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(long id)
        {
            try
            {
                var result = _service.Get(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public virtual IActionResult Post(T entity)
        {
            try
            {
                _service.Insert(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public virtual IActionResult Put(T entity)
        {
            try
            {
                var entityId = (entity as BaseEntity)?.Id ?? 0;
                var result = _service.Get(entityId);

                if (result == null)
                    return NotFound();

                _service.Update(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete("{id}")]
        public virtual IActionResult Delete(long id)
        {
            try
            {
                var result = _service.Get(id);

                if (result == null)
                    return NotFound();

                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
