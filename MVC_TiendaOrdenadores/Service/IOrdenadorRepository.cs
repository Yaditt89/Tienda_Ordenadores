using Microsoft.EntityFrameworkCore;
using MVC_TiendaOrdenadores.Models;

namespace MVC_TiendaOrdenadores.Service
{
    public interface IOrdenadorRepository
    {
        List<OrdenadorViewModel> AllOrdenador();
        void AddOrdenador(Ordenador? ordenador);
        void Edit(OrdenadorViewModel ordenador);
        void Delete(int id);
        OrdenadorViewModel GetOrdenadorViewModel(int id);
        List<Componente> GetComponentesPorTipo(EnumComponentes tipo);

    }
}
