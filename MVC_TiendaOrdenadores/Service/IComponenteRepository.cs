using MVC_TiendaOrdenadores.Models;

namespace MVC_TiendaOrdenadores.Service
{
    public interface IComponenteRepository
    {
        List<ComponenteViewModel> AllComponents();
        void AddComponents(ComponenteViewModel? comp);
        void Edit(ComponenteViewModel comp );
        void Delete(int id);
        bool HayOrdenadoresEnSistema();
        public ComponenteViewModel? GetComponente(int id);
    }
}
