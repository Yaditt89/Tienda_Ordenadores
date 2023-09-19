using Microsoft.AspNetCore.Mvc;
using MVC_TiendaOrdenadores.Models;
using MVC_TiendaOrdenadores.Service;

namespace MVC_TiendaOrdenadores.ViewComponents
{
    public class ComponenteViewComponent: ViewComponent
    {
        private readonly ISharedRepository _sharedRepository;

        public ComponenteViewComponent(ISharedRepository sharedRepository)
        {
            _sharedRepository = sharedRepository;
        }

        public IViewComponentResult Invoke(int componenteId)
        {
            var componente = _sharedRepository.GetComponente(componenteId);
            return View(componente);
        }
    }
}
