namespace TiendaOrdenadoresA.Comportamientos
{
    public class ConDescripcion : IDescripcion
    {
        public ConDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }

        public string Descripcion { get; set; }
    }
    }
