namespace BudgieBudgeting
{
    using System.Configuration;
    using System.Collections.Specialized;
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
            Console.WriteLine(_connectionString);
            Connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        }

        public Microsoft.Data.SqlClient.SqlConnection Connection { get; }
    }
}
