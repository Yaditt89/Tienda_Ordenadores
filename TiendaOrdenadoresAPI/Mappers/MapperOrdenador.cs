using Microsoft.Data.SqlClient;
using MVC_TiendaOrdenadores.Models;

namespace TiendaOrdenadoresAPI.Mappers
{
    public class MapperOrdenador : IMapper<Ordenador>
    {
        public Ordenador Map(SqlDataReader reader)
        {
            return new Ordenador
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"])
            };
        }
    }
}
