using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class Connection
{
    public string GetOutput()
    {
        string connectionString = "Server=tcp:budgie.database.windows.net,1433;Initial Catalog=Budgie Database;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM person", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string result = ds.Tables[0].Rows[0][0]?.ToString() ?? string.Empty;
                Console.WriteLine(result);
                return result;
            }
            Console.WriteLine("No data found.");
            return string.Empty;
        }
    }
}
