using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace BudgieBudgeting.DatabaseItems
{
    public class Database
    {
        List<Customer> customers;
        public Database() { 
            DataSet dataset = InitializeDatabase();
            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++) {
                customers.Add(new Customer(dataset,i));
            }
        }
        public DataSet InitializeDatabase()
        {
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            DataSet dataSet = new DataSet();
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", connection);
            SqlDataAdapter data = new SqlDataAdapter();
            DataTable table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Budget", connection);
            data = new SqlDataAdapter();
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Need", connection);
            data = new SqlDataAdapter();
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Saving", connection);
            data = new SqlDataAdapter();
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Want", connection);
            data = new SqlDataAdapter();
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            connection.Close();
            return dataSet;
        }
    }
}
