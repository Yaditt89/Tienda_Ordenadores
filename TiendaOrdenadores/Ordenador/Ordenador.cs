using TiendaOrdenadoresA.Componentes;

namespace TiendaOrdenadoresA.Ordenador
{
    public class Ordenador : IComponente
    {
        public IComponente Procesador { get; }
        public IComponente Ram { get; }
        public IComponente Disco { get; }

        public Ordenador(IComponente procesador, IComponente ram, IComponente disco)
        {
            Procesador = procesador;
            Ram = ram;
            Disco = disco;
        }



        public int Calor { get => Procesador.Calor + Ram.Calor + Disco.Calor; set => throw new NotImplementedException(); }

        public int Cores {get => Procesador.Cores + Ram.Cores + Disco.Cores; set => throw new NotImplementedException(); }
        public string Descripcion { get => "descripcion"; set => throw new NotImplementedException(); }
        public long Megas { get => Procesador.Megas + Ram.Megas + Disco.Megas; set => throw new NotImplementedException(); }
        public decimal Coste { get => Procesador.Coste + Ram.Coste + Ram.Coste; set => throw new NotImplementedException(); }
        public string NumeroSerie { get => "sn"; set => throw new NotImplementedException(); }
    }
}
