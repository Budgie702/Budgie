namespace BudgieBudgeting
{
    public static class TestDatabaseConnection
    {
        public static Microsoft.Data.SqlClient.SqlConnection Connection { get; } = new Microsoft.Data.SqlClient.SqlConnection("Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}
