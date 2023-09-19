using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_TiendaOrdenadores.Models;
using TiendaOrdenadoresAPI.Services;

namespace TiendaOrdenadoresAPI.Controllers
{
    [Route("api/[Controller]")]
    public class ComponenteController : Controller
    {
        readonly IRepository<Componente> _repositorio;

        public ComponenteController (IRepository<Componente> repositorio)
        {
            _repositorio = repositorio;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var componentes = _repositorio.GetAll();
            return Ok(componentes);
        }

        [HttpGet("tipo/{tipo}")]
        public IActionResult GetTipo(int tipo)
        {
            var componentes = _repositorio.GetComponentesPorTipo(tipo);
            return Ok(componentes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var componente = _repositorio.Get(id);

            if (componente == null)
            {
                return NotFound();
            }

            return Ok(componente);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Componente componente)
        {

            if (componente == null)
            {
                return BadRequest("El componente es nulo.");
            }

            _repositorio.Add(componente);
            return CreatedAtRoute("GetComponente", new { id = componente.Id }, componente);
        }

        [HttpPut()]
        public IActionResult Put([FromBody] Componente componente)
        {
            if (componente == null)
            {
                return BadRequest("El componente es nulo.");
            }

            if (_repositorio.Get(componente.Id) == null)
            {
                return NotFound();
            }

            _repositorio.Update(componente);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var componente = _repositorio.Get(id);

            if (componente == null)
            {
                return NotFound();
            }

            _repositorio.Delete(id);

            return Ok();
        }
    }
}
