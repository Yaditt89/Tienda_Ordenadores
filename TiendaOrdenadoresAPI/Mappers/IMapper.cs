using Microsoft.Data.SqlClient;

namespace TiendaOrdenadoresAPI.Mappers
{
    public interface IMapper<out T>
    {
        T Map(SqlDataReader reader);
    }
}
