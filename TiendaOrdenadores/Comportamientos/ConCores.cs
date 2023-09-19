namespace TiendaOrdenadoresA.Comportamientos
{
    public class ConCores : ICores
    {
        public ConCores(int cores)
        {
            Cores = cores;
        }

        public int Cores { get; set;}
    }
}