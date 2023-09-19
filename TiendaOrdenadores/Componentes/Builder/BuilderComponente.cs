using System.ComponentModel.DataAnnotations;
using TiendaOrdenadoresA.Componentes.Validador;
using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Componentes.Builder
{
    public class BuilderComponente : IBuilderComponente
    {
        public Componente? DameComponente(EnumComponente tipo)
        {
            return tipo switch
            {
                EnumComponente.ProcesadorInteli7_789_XCS => DameComponente("789_XCS", "Procesador Intel i7", 10, 0, 9,
                    134, EnumTipoComponente.Procesador),
                EnumComponente.ProcesadorInteli7_789_XCD => DameComponente("789_XCD", "Procesador Intel i7", 12, 0, 10,
                    138, EnumTipoComponente.Procesador),
                EnumComponente.ProcesadorInteli7_789_XCT => DameComponente("789_XCT", "Procesador Intel i7", 22, 0, 11,
                    138, EnumTipoComponente.Procesador),
                EnumComponente.BancoDeMemoriaSDRAM_879FH => DameComponente("879FH", "Banco de memoria SDRAM", 10, 512,
                    0, 100, EnumTipoComponente.MemoriaRAM),
                EnumComponente.BancoDeMemoriaSDRAM_879FH_L => DameComponente("879FH_L", "Banco de memoria SDRAM", 15,
                    1024, 0, 125, EnumTipoComponente.MemoriaRAM),
                EnumComponente.BancoDeMemoriaSDRAM_879FH_T => DameComponente("879FH_T", "Banco de memoria SDRAM", 24,
                    2028, 0, 150, EnumTipoComponente.MemoriaRAM),
                EnumComponente.DiscoDuroSanDisk_789_XX => DameComponente("789_XX", "Disco Duro Scan Disk", 10, 500000,
                    0, 50, EnumTipoComponente.Almacenamiento),
                EnumComponente.DiscoDuroSanDisk_789_XX_2 => DameComponente("789_XX_2", "Disco Duro Scan Disk", 29,
                    1000000, 0, 90, EnumTipoComponente.Almacenamiento),
                EnumComponente.DiscoDuroSanDisk_789_XX_3 => DameComponente("789_XX_3", "Disco Duro Scan Disk", 39,
                    2000000, 0, 128, EnumTipoComponente.Almacenamiento),
                _ => null
            };
        }

        public Componente? DameComponente(string serie, string descripcion, int calor, long megas, int cores, decimal coste, EnumTipoComponente tipo)
        {
            ValidationAttribute Validador = new ValidadorComponenteAttribute();
            ISerie miSerie;
            if (serie != "")
                miSerie = new ConSerie(serie);
            else
                miSerie = new SinSerie();

            IDescripcion miDescripcion;
            if (descripcion != "")
                miDescripcion = new ConDescripcion(descripcion);
            else
                miDescripcion = new SinDescripcion();

            ICalor miCalor;
            if (calor == 0)
                miCalor = new SinCalor();
            else
                miCalor = new ConCalor(calor);

            IMegas miMegas;
            if (megas == 0)
                miMegas = new SinMegas();
            else
                miMegas = new ConMegas(megas);


            ICores miCores;
            if (cores == 0)
                miCores = new SinCores();
            else
                miCores = new ConCores(cores);

            IPrecio miPrecio;
            if (coste == 0)
                miPrecio = new SinPrecio();
            else
                miPrecio = new ConPrecio(coste);

            Componente miComponente = new(miSerie, miDescripcion, miCalor, miMegas, miCores, miPrecio, tipo);
            if (Validador.IsValid(miComponente))
                return miComponente;
            else
                return null;
        }
    }
}
