namespace TiendaOrdenadoresA.Comportamientos
{
    public class ConCalor : ICalor
    {

        public ConCalor(int _calor)
        {
            Calor = _calor;
        }

        public int Calor { get; }
    }
}