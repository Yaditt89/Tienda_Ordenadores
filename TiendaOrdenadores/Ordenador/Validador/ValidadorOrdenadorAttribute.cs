using System.ComponentModel.DataAnnotations;
using TiendaOrdenadoresA.Componentes;
using TiendaOrdenadoresA.Componentes.Validador;
using TiendaOrdenadoresA.Comportamientos;

namespace TiendaOrdenadoresA.Ordenador.Validador
{
    public class ValidadorOrdenadorAttribute : ValidationAttribute
    {
        public override bool IsValid(Object? value)
        {
            var ordenador = value as Ordenador;
            if (ordenador is not null)
            {
                ValidationAttribute validador = new ValidadorComponenteAttribute();
                                
                return ((Componente)ordenador.Procesador).TipoComponente ==
                       EnumTipoComponente.Procesador &&
                       ((Componente)ordenador.Ram).TipoComponente ==
                       EnumTipoComponente.MemoriaRAM &&
                       ((Componente)ordenador.Disco).TipoComponente ==
                       EnumTipoComponente.Almacenamiento &&
                       validador.IsValid(ordenador.Procesador as Componente) &&
                       validador.IsValid(ordenador.Ram as Componente) &&
                       validador.IsValid(ordenador.Disco as Componente);

            }
            else
            {
                return false;
            }
        }
    }
}
