using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Project.Branef.Infrastructure.Data
{
    public class SQLDataContext : IDisposable
    {
        private readonly IConfiguration _configuration;

        public SQLDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection Connection { get; set; }

        public SqlConnection OpenConn()
        {
            Connection = new SqlConnection(_configuration.GetConnectionString("SQLDefaultConnection"));
            Connection.Open();

            return Connection;
        }

        public void Dispose()
        {
            if (Connection != null)
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
        }
    }
}
