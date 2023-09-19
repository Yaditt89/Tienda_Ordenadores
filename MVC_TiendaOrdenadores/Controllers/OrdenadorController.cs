using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_TiendaOrdenadores.Models;
using MVC_TiendaOrdenadores.Service;

namespace MVC_TiendaOrdenadores.Controllers
{
    public class OrdenadorController : Controller
    {
        private readonly IOrdenadorRepository _ordenadorRepository;
        private readonly IOrdenadorRepository _componenteRepository;

        public OrdenadorController(IOrdenadorRepository ordenadorRepository, IOrdenadorRepository componenteRepository)
        {
            _ordenadorRepository = ordenadorRepository;
            _componenteRepository = componenteRepository;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Create()
        {
            List<Componente> procesadores = _ordenadorRepository.GetComponentesPorTipo(EnumComponentes.Procesador);
            List<Componente> memorias = _ordenadorRepository.GetComponentesPorTipo(EnumComponentes.Memoria);
            List<Componente> discosDuros = _ordenadorRepository.GetComponentesPorTipo(EnumComponentes.DiscoDuro);

            ViewBag.Procesadores = procesadores;
            ViewBag.Memorias = memorias;
            ViewBag.DiscosDuros = discosDuros;

            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ordenador ordenador)
        {
                _ordenadorRepository.AddOrdenador(ordenador);
                return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
             OrdenadorViewModel? ordenador = _ordenadorRepository.GetOrdenadorViewModel(id);
            if (ordenador == null)
            {
                return NotFound();
            }
            return View("Delete", ordenador);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _ordenadorRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var procesadores = _ordenadorRepository.GetComponentesPorTipo(EnumComponentes.Procesador).Select(c => new { Value = c.Id, Text = $"{c.Serie} - {c.Descripcion}" })
    .ToList();
            var memorias = _ordenadorRepository.GetComponentesPorTipo(EnumComponentes.Memoria).Select(c => new { Value = c.Id, Text = $"{c.Serie} - {c.Descripcion}" })
    .ToList();
            var discosDuros = _ordenadorRepository.GetComponentesPorTipo(EnumComponentes.DiscoDuro).Select(c => new { Value = c.Id, Text = $"{c.Serie} - {c.Descripcion}" })
    .ToList();

            ViewBag.Procesadores = new SelectList(procesadores, "Value", "Text");
            ViewBag.Memorias = new SelectList(memorias, "Value", "Text");
            ViewBag.DiscosDuros = new SelectList(discosDuros, "Value", "Text");

            OrdenadorViewModel ordenador = _ordenadorRepository.GetOrdenadorViewModel(id);
            if (ordenador == null)
            {
                return NotFound();
            }

            return View(ordenador);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrdenadorViewModel ordenador)
        {
                ordenador.Id = id;
                _ordenadorRepository.Edit(ordenador);
                return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            OrdenadorViewModel? ordenador = _ordenadorRepository.GetOrdenadorViewModel(id);
            if (ordenador != null)
            {
                return View("Details", ordenador);
                
            }
            return NotFound();
        }

    }
}
