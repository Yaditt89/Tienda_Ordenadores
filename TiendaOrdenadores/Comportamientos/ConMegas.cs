namespace TiendaOrdenadoresA.Comportamientos
{
    public class ConMegas : IMegas
    {
        public ConMegas(long megas)
        {
            Megas = megas;
        }

        public long Megas { get; set; }
    }
}