using System.Data;
using Microsoft.Data.SqlClient;

namespace BudgieBudgeting.DatabaseItems
{
    public class Customer
    {
        private int CustomerId;
        private string Name;
        private string Email;
        private string Password;
        private float Income;
        private Budget budget;
        public Customer(DataSet dataset,int Customerplacement)
        {
            InitializeBudget(dataset, Customerplacement);
            for(int i = 0; i < dataset.Tables[0].Rows.Count; i++){
                SetCustomerId(Convert.ToInt32(dataset.Tables[0].Rows[i][0]));
                SetName(Convert.ToString(dataset.Tables[0].Rows[i][1]));
                SetPassword(Convert.ToString(dataset.Tables[0].Rows[i][2]));
                SetEmail(Convert.ToString(dataset.Tables[0].Rows[i][3]));
                SetIncome((float)Convert.ToDecimal(dataset.Tables[0].Rows[i][4]));
            }
        }
        //initializing budget
        private void InitializeBudget(DataSet dataset, int Customerplacement)
        {
            for (int i = 0; dataset.Tables[1].Rows.Count > 0; i++)
            {
                if (Convert.ToInt64(dataset.Tables[0].Rows[0][Customerplacement]) == Convert.ToInt64(dataset.Tables[0].Rows[i][1]))
                {
                    this.budget = new Budget(dataset, i);
                    break;
                }
            }
        }
        //setters
        private void SetCustomerId(int CustomerId)
        {
            this.CustomerId = CustomerId;
        }
        public void SetName(String Name)
        {
            this.Name = Name;
        }
        public void SetEmail(String Email)
        {
            this.Email = Email;
        }
        public void SetPassword(String Password)
        {
            this.Password = Password;
        }
        public void SetIncome(float Income)
        {
            this.Income = Income;
        }
        //Getters
        public int GetCustomerId()
        {
            return this.CustomerId;
        }
        public String GetName()
        {
            return this.Name;
        }
        public String GetEmail()
        {
            return this.Email;
        }
        public String GetPassword(){ 
            return this.Password; 
        }
        public float GetIncome()
        {
            return this.Income;
        }
        public Budget GetBudget()
        {
            return this.budget;
        }
        //Sql Stuff
        public void AddCustomer(DataTable table){
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("Insert into Customers(Customer,Username,Email,UserPassword,Income) values");
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateCustomer(){
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("Update Customer set Username = '" + GetName() + "', Email = '" + GetEmail() + "',UserPassword = '" + GetPassword() + "' Where CustomerId = " + GetCustomerId());
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void AddOrUpdateIncome(){
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("Update Customer set Income = "+ GetIncome() +" Where CustomerId = " + GetCustomerId());
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void RemoveCustomer(){
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("");
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        private int SetNewCustomerId(){
            string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "Select MAX(supplier_id) from suppliers";
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
