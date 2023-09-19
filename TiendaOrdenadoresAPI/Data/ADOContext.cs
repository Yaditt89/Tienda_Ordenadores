using Microsoft.Data.SqlClient;

namespace TiendaOrdenadoresAPI.Data
{
    public class AdoContext
    {
        private readonly SqlConnection _connection;

        public AdoContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Close();
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }
}