using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOrdenadoresA.Componentes.Validador
{
    public interface IValidadorComponente
    {
        bool isValid(Componente miComponente);
    }
}
