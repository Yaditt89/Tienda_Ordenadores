using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOrdenadoresA.Componentes.Validador
{
    public class ValidadorComponenteAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var miComponente = (Componente?)value;
            if (miComponente != null)
                return miComponente.Calor >= 0
                       && miComponente.NumeroSerie != ""
                       && miComponente is { Cores: >= 0, Coste: >= 0, Megas: >= 0 }
                       && miComponente.Descripcion != "";

            else
            {
                return false;
            }
        }


    }
}
