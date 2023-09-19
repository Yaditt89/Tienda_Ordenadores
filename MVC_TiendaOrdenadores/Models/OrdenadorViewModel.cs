namespace MVC_TiendaOrdenadores.Models
{
    public class OrdenadorViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Coste { get; set; }
        public int? Calor { get; set; }

        public List<Componente>? ComponentesLIst { get; set; }
    }
}
