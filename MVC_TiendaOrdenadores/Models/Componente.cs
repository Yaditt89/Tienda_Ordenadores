using System.ComponentModel.DataAnnotations;

namespace MVC_TiendaOrdenadores.Models
{
    public class Componente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 3, ErrorMessage = "El número de serie debe tener entre 3 y 8 caracteres.")]
        public string? Serie { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La descripción debe tener máximo 100 caracteres.")]
        public string? Descripcion { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El calor debe ser mayor o igual a 0.")]
        public int Calor { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Los megas deben ser mayor o igual a 0.")]
        public long Megas { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Los cores deben ser mayor o igual a 0.")]
        public int Cores { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El coste debe ser mayor o igual a 0.")]
        public int Coste { get; set; }

        [Required]
        public int Tipo { get; set; }

        public int? OrdenadorId { get; set; }
        public virtual Ordenador? Ordenador { get; set; }
    }
}
