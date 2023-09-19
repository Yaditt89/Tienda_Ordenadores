namespace MVC_TiendaOrdenadores.Models
{
    public class Ordenador
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Componente>? ComponentesLIst { get; set; }
    }
}
