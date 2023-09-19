using MVC_TiendaOrdenadores.Models;

namespace MVC_TiendaOrdenadores.Service
{
    public interface ISharedRepository
    {
        ComponenteViewModel? GetComponente(int id);
    }
}
