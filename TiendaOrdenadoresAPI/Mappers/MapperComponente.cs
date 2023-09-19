using Microsoft.Data.SqlClient;
using MVC_TiendaOrdenadores.Models;
using Newtonsoft.Json.Linq;

namespace TiendaOrdenadoresAPI.Mappers
{
    public class MapperComponente : IMapper<Componente>
    {
        public Componente Map(SqlDataReader reader)
        {
            return new Componente()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Calor = Convert.ToInt32(reader["Calor"]),
                Cores = Convert.ToInt32(reader["Cores"]),
                Coste = Convert.ToInt32(reader["Coste"]),
                Descripcion = Convert.ToString(reader["Descripcion"]),
                Megas = Convert.ToInt64(reader["Megas"]),
                Serie = Convert.ToString(reader["Serie"]) ?? "",
                Tipo = Convert.ToInt32(reader["Tipo"]),
                OrdenadorId = reader["OrdenadorId"] != DBNull.Value ? Convert.ToInt32(reader["OrdenadorId"]) : (int?)null,
            };
        }
    }
}

 