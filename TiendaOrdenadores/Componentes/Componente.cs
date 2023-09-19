using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Componentes
{
    public class Componente : IComponente
    {
        private readonly ISerie _Serie;
        private readonly IDescripcion _Descripcion;
        private readonly ICalor _Calor;
        private readonly IMegas _Megas;
        private readonly ICores _Cores;
        private readonly IPrecio _Precio;
        private readonly EnumTipoComponente _EnumTipoComponente;

        public Componente(ISerie serie, IDescripcion descripcion, ICalor calor, IMegas megas, ICores cores, IPrecio precio,
            EnumTipoComponente tipoComponente)
        {
            _Serie = serie;
            _Descripcion = descripcion;
            _Calor = calor;
            _Megas = megas;
            _Cores = cores;
            _Precio = precio;
            _EnumTipoComponente = tipoComponente;
        }

        public decimal Coste { get => _Precio.Coste; }
        public string NumeroSerie { get => _Serie.NumeroSerie; }
        public int Calor { get => _Calor.Calor; }
        public int Cores { get => _Cores.Cores;  }
        public string Descripcion { get => _Descripcion.Descripcion; }
        public long Megas { get => _Megas.Megas;  }
        public EnumTipoComponente TipoComponente { get => _EnumTipoComponente; }
    }
}
