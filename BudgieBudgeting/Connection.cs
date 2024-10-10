using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;
namespace BudgieBudgeting
{
    public class Conection
    {
        public Conection()
        {
            string connectionString = "Server=tcp:budgie.database.windows.net,1433;Initial Catalog=Budgie Database;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from person", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            connection.Close();
            Console.WriteLine(ds.Tables[0].Rows[0][0]);
        }
    }
}
