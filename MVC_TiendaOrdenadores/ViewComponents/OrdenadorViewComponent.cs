using Microsoft.AspNetCore.Mvc;
using MVC_TiendaOrdenadores.Service;

namespace MVC_TiendaOrdenadores.ViewComponents
{
    public class OrdenadorViewComponent: ViewComponent
    {
        private readonly IOrdenadorRepository _ordenadorRepository;

        public OrdenadorViewComponent(IOrdenadorRepository ordenadorRepository)
        {
            _ordenadorRepository = ordenadorRepository;
        }

        public IViewComponentResult Invoke()
        {
            var ordenadores = _ordenadorRepository.AllOrdenador();
            return View(ordenadores);
        }
    }
}
