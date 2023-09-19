namespace TiendaOrdenadoresA.Comportamientos
{
    public class ConSerie : ISerie
    {
        public ConSerie(string _serie)
        {
            NumeroSerie = _serie;
        }

        public string NumeroSerie { get; set; }
    }
}