namespace BudgieBudgeting
{
    using System.Configuration;
    using System.Collections.Specialized;
    public static class DatabaseConnection
    {
        static IConfigurationRoot Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        static string? ConnectionString = Configuration.GetConnectionString("BudgieBudgetingContext");
        public static Microsoft.Data.SqlClient.SqlConnection Connection { get; } = new Microsoft.Data.SqlClient.SqlConnection(ConnectionString!);
    }
}
