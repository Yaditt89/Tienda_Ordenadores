using Microsoft.Data.SqlClient;
using MVC_TiendaOrdenadores.Models;
using TiendaOrdenadoresAPI.Data;
using TiendaOrdenadoresAPI.Mappers;

namespace TiendaOrdenadoresAPI.Services
{
    public class RepositoryComponente : IRepository<Componente>
    {
        readonly SqlConnection conexion;
        readonly IMapper<Componente> mapper;

        public RepositoryComponente(AdoContext contexto)
        {
            conexion = contexto.GetConnection();
            mapper = new MapperComponente();
        }

        public List<Componente> GetAll()
        {
            var componentes = new List<Componente>();
            string sql = "SELECT c.*, o.* " +
                         "FROM Componente c " +
                         "LEFT JOIN Ordenador o ON c.OrdenadorId = o.Id";

            using (SqlCommand command = new(sql, conexion))
            {
                conexion.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var componente = mapper.Map(dataReader);
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("OrdenadorId")))
                    {
                        componente.Ordenador = new Ordenador
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            Name = dataReader.GetString(dataReader.GetOrdinal("Name"))
                        };
                    }

                    componentes.Add(componente);
                }
                conexion.Close();
            }
            return componentes;
        }


        public void Add(Componente item)
        {
            string sql = "INSERT INTO Componente (Serie, Descripcion, Calor, Megas, Cores, Coste, Tipo, OrdenadorId) " +
                        "VALUES (@Serie, @Descripcion, @Calor, @Megas, @Cores, @Coste, @Tipo, @OrdenadorId)";

            using SqlCommand command = new(sql, conexion);
            command.Parameters.AddWithValue("@Serie", item.Serie);
            command.Parameters.AddWithValue("@Descripcion", item.Descripcion);
            command.Parameters.AddWithValue("@Calor", item.Calor);
            command.Parameters.AddWithValue("@Megas", item.Megas);
            command.Parameters.AddWithValue("@Cores", item.Cores);
            command.Parameters.AddWithValue("@Coste", item.Coste);
            command.Parameters.AddWithValue("@Tipo", item.Tipo);
            command.Parameters.AddWithValue("@OrdenadorId", item.OrdenadorId ?? (object)DBNull.Value);

            conexion.Open();
            command.ExecuteNonQuery();
            conexion.Close();
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Componente WHERE Id = @Id";

            using SqlCommand command = new(sql, conexion);
            command.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            command.ExecuteNonQuery();
            conexion.Close();
        }

        public void Update(Componente element)
        {
            string sql = "UPDATE Componente " +
                          "SET Serie = @Serie, Descripcion = @Descripcion, Calor = @Calor, " +
                          "Megas = @Megas, Cores = @Cores, Coste = @Coste, Tipo = @Tipo, " +
                          "OrdenadorId = @OrdenadorId " +
                          "WHERE Id = @Id";

            using SqlCommand command = new(sql, conexion);
            command.Parameters.AddWithValue("@Id", element.Id);
            command.Parameters.AddWithValue("@Serie", element.Serie);
            command.Parameters.AddWithValue("@Descripcion", element.Descripcion);
            command.Parameters.AddWithValue("@Calor", element.Calor);
            command.Parameters.AddWithValue("@Megas", element.Megas);
            command.Parameters.AddWithValue("@Cores", element.Cores);
            command.Parameters.AddWithValue("@Coste", element.Coste);
            command.Parameters.AddWithValue("@Tipo", element.Tipo);
            command.Parameters.AddWithValue("@OrdenadorId", element.OrdenadorId ?? (object)DBNull.Value);

            conexion.Open();
            command.ExecuteNonQuery();
            conexion.Close();
        }

        public Componente? Get(int id)
        {
            Componente componente = null;
            string sql = "SELECT c.*, o.* " +
                         "FROM Componente c " +
                         "LEFT JOIN Ordenador o ON c.OrdenadorId = o.Id " +
                         "WHERE c.Id = @Id";

            using (SqlCommand command = new(sql, conexion))
            {
                command.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (componente == null)
                    {
                        componente = mapper.Map(dataReader);

                        // Verificar si el componente tiene un ordenador asociado
                        if (!dataReader.IsDBNull(dataReader.GetOrdinal("OrdenadorId")))
                        {
                            componente.Ordenador = new Ordenador
                            {
                                Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                Name = dataReader.GetString(dataReader.GetOrdinal("Name"))
                            };
                        }
                    }
                }
                conexion.Close();
            }

            return componente;
        }

        public List<Componente> GetComponentesPorTipo(int tipo)
        {
            try
            {
                string sql = "SELECT * FROM Componente WHERE Tipo = @Tipo AND OrdenadorId IS NULL";
                using (SqlCommand command = new(sql, conexion))
                {
                    command.Parameters.AddWithValue("@Tipo", tipo);

                    List<Componente> componentes = new();

                    conexion.Open();
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Componente componente = mapper.Map(dataReader);
                        componentes.Add(componente);
                    }

                    conexion.Close();

                    return componentes;
                }
            }
            catch (Exception ex)
            {
                // Agrega registro de errores o manejo de excepciones aquí
                Console.WriteLine("Error en GetComponentesPorTipo: " + ex.Message);
                return new List<Componente>();
            }
        }

    }
}