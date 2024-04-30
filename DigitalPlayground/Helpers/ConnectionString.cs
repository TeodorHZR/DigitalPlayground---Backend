using DigitalPlayground.Business;
using System.Data.SqlClient;
namespace DigitalPlayground.Helpers
{
    public class ConnectionString : IConnectionString
    {
        public ConnectionString(string connectionString)
        {
            SqlConnectionString = connectionString;
        }

        public string SqlConnectionString { get; private set; }
    }
}
