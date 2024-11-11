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
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Budget", connection);
            data = new SqlDataAdapter(cmd);
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Need", connection);
            data = new SqlDataAdapter(cmd);
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Saving", connection);
            data = new SqlDataAdapter(cmd);
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            cmd = new SqlCommand("SELECT * FROM Want", connection);
            data = new SqlDataAdapter(cmd);
            table = new DataTable();
            data.Fill(table);
            dataSet.Tables.Add(table);

            connection.Close();
            return dataSet;
        }

        public void AddCustomer(DataTable table){
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("Insert into Customers(CustomerId,Username,Email,UserPassword) values(" +SetNewCustomerId() + "'," + table.Rows[0][0] + "'," + table.Rows[0][1] + "'," + table.Rows[0][2] + "'");
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private static int SetNewCustomerId(){
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "Select MAX(CustomerId) from Customer";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            connection.Close();
            if (ds.Tables[0].Rows[0][0] != null)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
