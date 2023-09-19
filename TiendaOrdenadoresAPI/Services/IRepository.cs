namespace TiendaOrdenadoresAPI.Services
{
    public interface IRepository<T>
    {
            List<T> GetAll();
            void Add(T item);
            void Delete(int id);
            void Update(T element);
            T? Get(int id);
            List<T> GetComponentesPorTipo(int tipo);
    }
}


