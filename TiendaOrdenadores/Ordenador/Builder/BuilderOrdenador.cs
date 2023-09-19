using System.Diagnostics.CodeAnalysis;
using TiendaOrdenadoresA.Componentes;
using TiendaOrdenadoresA.Componentes.Builder;
using TiendaOrdenadoresA.Comportamientos;
using TiendaOrdenadoresA.Ordenador.Validador;

namespace TiendaOrdenadoresA.Ordenador.Builder
{
    public class BuilderOrdenador : IBuilderOrdenador
    {
        public Ordenador? DameOrdenador(EnumOrdenadoresTipo tipo)
        {
            Ordenador? miOrdenador = null;
            ValidadorOrdenadorAttribute validador = new ();
            switch (tipo)
            {
                case EnumOrdenadoresTipo.OrdenadorMaria:
                    BuilderComponente builder = new();
                    IComponente? procesador = builder.DameComponente(EnumComponente.ProcesadorInteli7_789_XCS);
                    IComponente? ram = builder.DameComponente(EnumComponente.BancoDeMemoriaSDRAM_879FH);
                    IComponente? disco = builder.DameComponente(EnumComponente.DiscoDuroSanDisk_789_XX);
                    if (procesador != null && ram != null && disco != null)
                        miOrdenador = new Ordenador(procesador, ram, disco);
                    break;
            }
            if (miOrdenador is not null)
            {
                if (validador.IsValid(miOrdenador))
                    return miOrdenador;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public Ordenador? DameOrdenador(IComponente procesador, IComponente ram, IComponente disco)
        {
            Ordenador miOrdenador = new(procesador, ram, disco);
            ValidadorOrdenadorAttribute validador = new();
            if (validador.IsValid(miOrdenador)) 
                return miOrdenador;
            else 
                return null;
        }
    }
}
