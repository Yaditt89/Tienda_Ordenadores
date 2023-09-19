using TiendaOrdenadoresA.Componentes;
using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Ordenador.Builder
{
    public interface IBuilderOrdenador
    {

        Ordenador? DameOrdenador(EnumOrdenadoresTipo tipo);

        public Ordenador? DameOrdenador(IComponente procesador, IComponente ram, IComponente disco);

    }
}

