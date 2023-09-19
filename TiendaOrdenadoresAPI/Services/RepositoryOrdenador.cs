using Microsoft.Data.SqlClient;
using MVC_TiendaOrdenadores.Models;
using System.Transactions;
using TiendaOrdenadoresAPI.Data;
using TiendaOrdenadoresAPI.Mappers;

namespace TiendaOrdenadoresAPI.Services
{
    public class RepositoryOrdenador : IRepository<Ordenador>
    {
        readonly SqlConnection conexion;
        readonly IMapper<Ordenador> mapper;
        readonly IMapper<Componente> mapperComp;

        public RepositoryOrdenador(AdoContext contexto)
        {
            conexion = contexto.GetConnection();
            mapper = new MapperOrdenador();
            mapperComp = new MapperComponente();
        }
        public void Add(Ordenador item)
        {
            string insertOrdenadorSql = "INSERT INTO Ordenador (Name) VALUES (@Name); SELECT SCOPE_IDENTITY()";

            using SqlCommand insertOrdenadorCommand = new (insertOrdenadorSql, conexion);
            insertOrdenadorCommand.Parameters.AddWithValue("@Name", item.Name);

            conexion.Open();

            using SqlTransaction transaction = conexion.BeginTransaction();
            insertOrdenadorCommand.Transaction = transaction;

            try
            {
                // Insertar el nuevo ordenador y obtener su ID
                int nuevoOrdenadorId = Convert.ToInt32(insertOrdenadorCommand.ExecuteScalar());

                foreach (var componente in item.ComponentesLIst!)
                {
                    string updateComponenteSql = "UPDATE Componente SET OrdenadorId = @OrdenadorId WHERE Id = @ComponenteId";

                    using SqlCommand updateComponenteCommand = new(updateComponenteSql, conexion);
                    updateComponenteCommand.Transaction = transaction;
                    updateComponenteCommand.Parameters.AddWithValue("@OrdenadorId", nuevoOrdenadorId);
                    updateComponenteCommand.Parameters.AddWithValue("@ComponenteId", componente.Id);
                    updateComponenteCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void Delete(int id)
        {
            string deleteOrdenadorSql = "DELETE FROM Ordenador WHERE Id = @Id";
            string updateComponentesSql = "UPDATE Componente SET OrdenadorId = NULL WHERE OrdenadorId = @Id";

            using SqlCommand deleteOrdenadorCommand = new(deleteOrdenadorSql, conexion);
            using SqlCommand updateComponentesCommand = new(updateComponentesSql, conexion);
            deleteOrdenadorCommand.Parameters.AddWithValue("@Id", id);
            updateComponentesCommand.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            using (SqlTransaction transaction = conexion.BeginTransaction())
            {
                try
                {
                    deleteOrdenadorCommand.Transaction = transaction;
                    updateComponentesCommand.Transaction = transaction;

                    updateComponentesCommand.ExecuteNonQuery();
                    deleteOrdenadorCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            conexion.Close();
        }

        public Ordenador? Get(int id)
        {
            string sql = "SELECT o.Id AS OrdenadorId, o.Name, c.Id AS ComponenteId, c.* FROM Ordenador o " +
                 "LEFT JOIN Componente c ON o.Id = c.OrdenadorId " +
                 "WHERE o.Id = @OrdenadorId"; // Cambio @Id a @OrdenadorId

            //string sql = "SELECT o.Id, o.Name, c.Id AS ComponenteId, c.* FROM Ordenador o " +
            //             "LEFT JOIN Componente c ON o.Id = c.OrdenadorId " + "WHERE o.Id = @Id";

            Ordenador? ordenador = null;

            using (SqlCommand command = new(sql, conexion))
            {
                command.Parameters.AddWithValue("@OrdenadorId", id); // Cambio @Id a @OrdenadorId
                conexion.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    if (ordenador == null)
                    {
                        ordenador = mapper.Map(dataReader);
                        ordenador.ComponentesLIst = new List<Componente>();
                    }

                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("ComponenteId")))
                    {
                        Componente componente = mapperComp.Map(dataReader);
                        ordenador.ComponentesLIst!.Add(componente);
                    }
                }

                conexion.Close();
            }

            return ordenador;
        }

        public List<Ordenador> GetAll()
        {
            var ordenadores = new Dictionary<int, Ordenador>();
            string sql = "SELECT o.Id, o.Name, c.Id AS ComponenteId, c.* FROM Ordenador o " +
                         "LEFT JOIN Componente c ON o.Id = c.OrdenadorId";

            using (SqlCommand command = new(sql, conexion))
            {
                conexion.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int ordenadorId = dataReader.IsDBNull(dataReader.GetOrdinal("OrdenadorId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("OrdenadorId"));

                    if (!ordenadores.TryGetValue(ordenadorId, out var ordenador))
                    {
                        ordenador = mapper.Map(dataReader);
                        ordenador.ComponentesLIst = new List<Componente>();
                        ordenadores.Add(ordenadorId, ordenador);
                    }

                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("ComponenteId")))
                    {
                        Componente componente = mapperComp.Map(dataReader);
                        ordenador.ComponentesLIst!.Add(componente);
                    }
                }

                conexion.Close();
            }

            return ordenadores.Values.ToList();
        }

        public List<Ordenador> GetComponentesPorTipo(int tipo)
        {
            throw new NotImplementedException();
        }

        public void Update(Ordenador element)
        {
            string updateOrdenadorSql = "UPDATE Ordenador SET Name = @Name WHERE Id = @Id";
            string updateComponentesSql = "UPDATE Componente SET OrdenadorId = NULL WHERE OrdenadorId = @Id";

            using (SqlCommand updateOrdenadorCommand = new(updateOrdenadorSql, conexion))
            using (SqlCommand updateComponentesCommand = new(updateComponentesSql, conexion))
            {
                updateOrdenadorCommand.Parameters.AddWithValue("@Id", element.Id);
                updateOrdenadorCommand.Parameters.AddWithValue("@Name", element.Name);

                updateComponentesCommand.Parameters.AddWithValue("@Id", element.Id);

                conexion.Open();

                using (SqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        updateOrdenadorCommand.Transaction = transaction;
                        updateComponentesCommand.Transaction = transaction;

                        updateOrdenadorCommand.ExecuteNonQuery();
                        updateComponentesCommand.ExecuteNonQuery();

                        // Agregar los componentes que vienen por parámetro
                        foreach (var componente in element.ComponentesLIst)
                        {
                            // Actualiza el OrdenadorId de los componentes relacionados
                            string updateComponenteSql = "UPDATE Componente SET OrdenadorId = @OrdenadorId WHERE Id = @ComponenteId";
                            using (SqlCommand updateComponenteCommand = new(updateComponenteSql, conexion))
                            {
                                updateComponenteCommand.Parameters.AddWithValue("@ComponenteId", componente.Id);
                                updateComponenteCommand.Parameters.AddWithValue("@OrdenadorId", element.Id);

                                updateComponenteCommand.Transaction = transaction;
                                updateComponenteCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                conexion.Close();
            }
        }
        }
}
