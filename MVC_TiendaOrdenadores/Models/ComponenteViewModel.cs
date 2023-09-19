using System.ComponentModel.DataAnnotations;

namespace MVC_TiendaOrdenadores.Models
{
    public class ComponenteViewModel
    {
        public int Id { get; set; }
        public string? Serie { get; set; }

        public string? Descripcion { get; set; }

        public int Calor { get; set; }

        public long Megas { get; set; }

        public int Cores { get; set; }

        public int Coste { get; set; }

        public EnumComponentes Tipo { get; set; }

        public int? OrdenadorId { get; set; }

        public  Ordenador? Ordenador { get; set; }
    }
}
