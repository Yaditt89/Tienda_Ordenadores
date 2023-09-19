using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_TiendaOrdenadores.Models;
using MVC_TiendaOrdenadores.Service;

namespace MVC_TiendaOrdenadores.Controllers
{
    public class ComponenteController : Controller
    {
        private readonly IComponenteRepository _componentesRepository;
        private readonly IOrdenadorRepository _ordenadorRepository;
 



        public ComponenteController(IComponenteRepository componentesRepository, IOrdenadorRepository ordenadorRepository)
        {
            _componentesRepository = componentesRepository;
            _ordenadorRepository = ordenadorRepository;
        }

        public IActionResult Index()
        {
            return View("Index", _componentesRepository.AllComponents());
        }
        public ActionResult Create()
        {
           // ViewBag.OrdenadorIdVisible = _componentesRepository.HayOrdenadoresEnSistema();
            //var ordenadores = GetListaOrdenadores(_ordenadorRepository.AllOrdenador());
           // ordenadores.Insert(0, new SelectListItem { Value = "", Text = "Seleccione un Ordenador" });
            //ViewBag.Ordenadores = ordenadores;
            ViewData["EnumComponenteOptions"] = GetEnumComponenteOptions();
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComponenteViewModel comp)
        {
            if (ModelState.IsValid)
            {
                //_componentesRepository.HayOrdenadoresEnSistema();
                _componentesRepository.AddComponents(comp);
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult Edit(int id)
        {
            ComponenteViewModel? comp = _componentesRepository.GetComponente(id);
            if (comp == null)
            {
                return NotFound();
            }
            //var ordenadores = GetListaOrdenadores(_ordenadorRepository.AllOrdenador());
            //ordenadores.Insert(0, new SelectListItem { Value = "", Text = "No Asignar" });
            //ViewBag.OrdenadorIdVisible = _componentesRepository.HayOrdenadoresEnSistema();
            //ViewBag.Ordenadores = ordenadores;
            ViewData["EnumComponenteOptions"] = GetEnumComponenteOptions();
            return View("Edit", comp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ComponenteViewModel comp)
        {
            if (ModelState.IsValid)
            {
                //_componentesRepository.HayOrdenadoresEnSistema();
                comp.Id = id;
                _componentesRepository.Edit(comp);
                return RedirectToAction("Index");
            }

            return View(comp);
        }

        public ActionResult Delete(int id)
        {
            ComponenteViewModel? comp = _componentesRepository.GetComponente(id);
            if (comp == null)
            {
                return NotFound();
            }
            return View("Delete", comp);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            ComponenteViewModel? comp = _componentesRepository.GetComponente(id);
            if (comp == null)
            {
                return NotFound();
            }
            _componentesRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            ComponenteViewModel? comp = _componentesRepository.GetComponente(id);
            if (comp != null)
            {
                
                return View("Details", comp);
            }
            return NotFound();
        }

        private static List<SelectListItem> GetEnumComponenteOptions()
        {
            return Enum.GetValues(typeof(EnumComponentes))
                .Cast<EnumComponentes>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                })
                .ToList();
        }

        private static List<SelectListItem> GetListaOrdenadores(List<OrdenadorViewModel> ordenadores)
        {
            return ordenadores
                .Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),   
                    Text = o.Name             
                })
                .ToList();
        }
    }
}

