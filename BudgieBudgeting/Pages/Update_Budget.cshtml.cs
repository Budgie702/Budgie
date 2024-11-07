using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for accessing cookies
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace BudgieBudgeting.Pages
{
    public class Update_BudgetModel : PageModel
    {
        private readonly DatabaseConnection _databaseConnection;
        public Update_BudgetModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        // Properties to hold data for Needs, Wants, and Savings
        public List<Need> Needs = new List<Need>();
        public List<Want> Wants = new List<Want>();
        public List<Saving> Savings= new List<Saving>();

        public string? Username { get; private set; } // Property to hold the username

        // This method runs when the page is loaded
        public void OnGet()
        {
            // Retrieve the username from the cookie
            Username = HttpContext.Session.GetString("Username"); // Use the same key you used when setting the session
            string query = "Select dbo.Budget.BudgetId from inner join dbo.Budget on dbo.Customer.CustomerId = dbo.Budget.CustomerId where @Username = dbo.Customer.Username";
            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                DataTable table = new DataTable();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    connection.Close();
                    dataAdapter.Fill(table);
                }
                int BudgetId = Convert.ToInt32(table.Rows[0][0]);
                SetNeeds(connection,BudgetId);
                SetWants(connection, BudgetId);
                SetSavings(connection, BudgetId);
            }

        }
        private void SetNeeds(SqlConnection connection,int BudgetId)
        {
            String query = "Select dbo.NeedDetails.NeedDetailID,dbo.NeedDetails.NeedName,dbo.NeedDetails.NeedValue from inner join dbo.Need on dbo.NeedDetails.NeedID = dbo.NeedID Where @BudgetId = dbo.Need.BudgetId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                DataTable table = new DataTable();
                command.Parameters.AddWithValue("@BudgetId", BudgetId);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Close();
                adapter.Fill(table);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Need need = new Need();
                    need.NeedDetailId = Convert.ToInt32(table.Rows[i][0]);
                    need.NeedName = table.Rows[i][1].ToString();
                    need.NeedValue = (float)Convert.ToDouble(table.Rows[i][2]);
                    Needs.Add(need);
                }
            }
        }
        private void SetWants(SqlConnection connection, int BudgetId)
        {
            String query = "Select dbo.WantDetails.WantDetailID,dbo.WantDetails.WantName,dbo.WantDetails.WantValue from inner join dbo.Want on dbo.WantDetails.WantID = dbo.WantID Where @BudgetId = dbo.Want.BudgetId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                DataTable table = new DataTable();
                command.Parameters.AddWithValue("@BudgetId", BudgetId);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Close();
                adapter.Fill(table);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Want want = new Want();
                    want.WantDetailId = Convert.ToInt32(table.Rows[i][0]);
                    want.WantName = table.Rows[i][1].ToString();
                    want.WantValue = (float)Convert.ToDouble(table.Rows[i][2]);
                    this.Wants.Add(want);
                }
            }
        }
        private void SetSavings(SqlConnection connection, int BudgetId)
        {
            String query = "Select dbo.SavingDetails.SavingDetailID,dbo.SavingDetails.SavingName,dbo.SavingDetails.SavingValue from inner join dbo.Saving on dbo.SavingDetails.SavingID = dbo.SavingID Where @BudgetId = dbo.Saving.BudgetId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                DataTable table = new DataTable();
                command.Parameters.AddWithValue("@BudgetId", BudgetId);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Close();
                adapter.Fill(table);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Saving savings = new Saving();
                    savings.SavingDetailId = Convert.ToInt32(table.Rows[i][0]);
                    savings.SavingName = table.Rows[i][1].ToString();
                    savings.SavingValue = (float)Convert.ToDouble(table.Rows[i][2]);
                    Savings.Add(savings);
                }
            }
        }
        // This method handles the form submission when the update button is clicked
        public IActionResult OnPost(List<Need> UpdatedNeeds, List<Want> UpdatedWants, List<Saving> UpdatedSavings)
        {
            // TODO: Connect to your database here to update the Needs, Wants, and Savings
            // You can use the Username variable for your queries
            /*
            using (var context = new YourDbContext())
            {
                // Clear existing items for the user
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Need" && item.Username == Username));
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Want" && item.Username == Username));
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Saving" && item.Username == Username));

                // Add new items for the user
                foreach (var need in Needs)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Need", Description = need, Username = Username });
                }
                foreach (var want in Wants)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Want", Description = want, Username = Username });
                }
                foreach (var saving in Savings)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Saving", Description = saving, Username = Username });
                }

                await context.SaveChangesAsync(); // Save changes to the database
            }
            */
            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                UpdateNeeds(UpdatedNeeds,connection);
                UpdateWants(UpdatedWants,connection);
                UpdateSavings(UpdatedSavings,connection);
            }
                return RedirectToPage(); // Redirect back to the same page
        }
        public static void UpdateNeeds(List<Need> UpdatedNeeds,SqlConnection connection)
        {
            String query = "Update NeedDetails set NeedName = @NeedName,NeedValue = @NeedValue where NeedDetailID = @NeedDetailID";
            for (int i = 0; i < UpdatedNeeds.Count; i++)
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@NeedName", UpdatedNeeds[i].NeedName);
                    command.Parameters.AddWithValue("@NeedValue", UpdatedNeeds[i].NeedValue);
                    command.Parameters.AddWithValue("@NeedDetailID", UpdatedNeeds[i].NeedDetailId);

                }
            }
        }
        public static void UpdateWants(List<Want> UpdatedWants, SqlConnection connection)
        {
            String query = "Update WantDetails set WantName = @WantName,WantValue = @WantValue where WantDetailID = @WantDetailID";
            for (int i = 0; i < UpdatedWants.Count; i++)
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@WantName", UpdatedWants[i].WantName);
                    command.Parameters.AddWithValue("@WantValue", UpdatedWants[i].WantValue);
                    command.Parameters.AddWithValue("@WantDetailID", UpdatedWants[i].WantDetailId);

                }
            }
        }
        public static void UpdateSavings(List<Saving> UpdatedSavings, SqlConnection connection)
        {
            String query = "Update SavingDetails set SavingName = @SavingName,SavingValue = @SavingValue where SavingDetailID = @SavingDetailID"; 
            for (int i = 0; i < UpdatedSavings.Count; i++)
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@SavingName", UpdatedSavings[i].SavingName);
                    command.Parameters.AddWithValue("@SavingValue", UpdatedSavings[i].SavingValue);
                    command.Parameters.AddWithValue("@SavingDetailID", UpdatedSavings[i].SavingDetailId);

                }
            }
        }
    }
    public class Need()
    {
        [Required]
        public int NeedDetailId { get; set; }
        [Required]
        public string NeedName { get; set; }
        [Required]
        public float NeedValue {  get; set; }
    }
    public class Want()
    {
        [Required]
        public int WantDetailId { get; set; }
        [Required]
        public string WantName { get; set; }
        [Required]
        public float WantValue { get; set; }
    }
    public class Saving()
    {
        [Required]
        public int SavingDetailId { get; set; }
        [Required]
        public string SavingName { get; set; }
        [Required]
        public float SavingValue { get; set; }
    }
}
