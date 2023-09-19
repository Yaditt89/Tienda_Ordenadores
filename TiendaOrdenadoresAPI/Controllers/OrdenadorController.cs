using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_TiendaOrdenadores.Models;
using TiendaOrdenadoresAPI.Services;

namespace TiendaOrdenadoresAPI.Controllers
{
    [Route("api/[Controller]")]
    public class OrdenadorController : Controller
    {
        private readonly IRepository<Ordenador> _repositorio;

        public OrdenadorController(IRepository<Ordenador> repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ordenadores = _repositorio.GetAll();
            return Ok(ordenadores);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ordenador = _repositorio.Get(id);

            if (ordenador == null)
            {
                return NotFound();
            }

            return Ok(ordenador);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ordenador ordenador)
        {
            if (ordenador == null)
            {
                return BadRequest("El ordenador es nulo.");
            }

            _repositorio.Add(ordenador);
            return CreatedAtRoute("GetOrdenador", new { id = ordenador.Id }, ordenador);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Ordenador ordenador)
        {
            if (ordenador == null)
            {
                return BadRequest("El ordenador es nulo.");
            }

            var existingOrdenador = _repositorio.Get(ordenador.Id);

            if (existingOrdenador == null)
            {
                return NotFound();
            }
            _repositorio.Update(ordenador);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ordenador = _repositorio.Get(id);

            if (ordenador == null)
            {
                return NotFound();
            }

            _repositorio.Delete(id);

            return Ok();
        }
    }
}
