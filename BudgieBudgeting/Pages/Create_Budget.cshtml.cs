using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for accessing sessions
using System.Collections.Generic;
using BudgieBudgeting.DatabaseItems;
using Microsoft.Data.SqlClient;
using BudgieBudgeting.Pages.Shared;
using Castle.Core.Resource;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BudgieBudgeting.Pages
{
    public class Create_BudgetModel : PageModel
    {
        [BindProperty]
        public List<string> Needs { get; set; } = new List<string>();

        [BindProperty]
        public List<string> Wants { get; set; } = new List<string>();

        [BindProperty]
        public List<string> Savings { get; set; } = new List<string>();

        public string? Username { get; private set; }
        public int BudgetID { get; private set; }
        public object CustomerID { get; private set; }
        public object Income { get; private set; }
        public double NeedsBudget { get; private set; }
        public double WantsBudget { get; private set; }
        public double SavingsBudget { get; private set; }
        public int NeedID { get; private set; }
        public int WantID { get; private set; }
        public int SavingID { get; private set; }
        public string ErrorMessage { get; private set; }
        public int NeedDetailsID { get; private set; }
        public int NeedDetailID { get; private set; }

        private readonly DatabaseConnection _databaseConnection;

        public Create_BudgetModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username");

            // Optional: Check if username is null or empty
            if (string.IsNullOrEmpty(Username))
            {
                RedirectToPage("/Login");
            }
        }

        public IActionResult OnPost()
        {
            InsertBudget();
            InsertNeed();
            InsertNeedDetails();
            return RedirectToPage("/Success");
        }

        public decimal getCustomerIncome()
        {
            Username = HttpContext.Session.GetString("Username");
            string getCustomerIncomeQuery = "SELECT Income FROM dbo.Customer Where Username like '" + Username + "'";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getCustomerIncomeQuery, connection))
                {
                    using (SqlDataReader reader = getCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetDecimal(0);

                        }
                        else
                        {
                            ErrorMessage = "Error: No income found for the customer.";
                            return -1;
                        }
                    }
                }
            }
        }

        public int getCustomerID()
        {
            string Username = HttpContext.Session.GetString("Username");
            // SQL query to get the CustomerID 
            string GetCustomerID = "SELECT CustomerId FROM dbo.Customer Where Username like '" + Username + "'";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(GetCustomerID, connection))
                {
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    using (SqlDataReader reader = getCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            ErrorMessage = "Error: No CustomerID was found.";
                            return -1;
                        }
                    }
                }
            }
        }

        //public int getNeedDetailsID()
        //{
        //    // SQL query to get the max NeedDetailID
        //    string getMaxNeedDetailsIDQuery = "SELECT MAX(NeedDetailID) FROM dbo.NeedDetails";

        //    using (SqlConnection connection = _databaseConnection.Connection)
        //    {
        //        connection.Open();
        //        using (SqlCommand getCommand = new SqlCommand(getMaxNeedDetailsIDQuery, connection))
        //        {
        //            // Add parameters for income and username 
        //            getCommand.Parameters.AddWithValue("@NeedDetailID", NeedDetailID);
        //            int rowsAffected = getCommand.ExecuteNonQuery();
        //            if (rowsAffected == 0)
        //            {
        //                ErrorMessage = "Error: No NeedDetailID was found.";
        //                return -1;
        //            }

        //            else
        //            {
        //                return @NeedDetailID;
        //            }
        //        }
        //    }
        //}

        public void InsertBudget()
        {
            decimal income = getCustomerIncome();
            decimal needsBudget = income * 0.5m;
            decimal wantsBudget = income * 0.3m;
            decimal savingsBudget = income * 0.2m;

            string insertQuery = "INSERT INTO dbo.Budget (CustomerId, NeedsBudget, WantsBudget, SavingsBudget) VALUES (@CustomerID, @NeedsBudget, @WantsBudget, @SavingsBudget)";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@CustomerID", getCustomerID());
                    insertCommand.Parameters.AddWithValue("@NeedsBudget", needsBudget);
                    insertCommand.Parameters.AddWithValue("@WantsBudget", wantsBudget);
                    insertCommand.Parameters.AddWithValue("@SavingsBudget", savingsBudget);
                    insertCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void InsertNeed()
        {
            string insertNeedTable = "INSERT INTO dbo.Need (BudgetID) VALUES (@BudgetID)";
            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();
                
                using (SqlCommand insertCommand = new SqlCommand(insertNeedTable, connection))
                {
                    insertCommand.Parameters.AddWithValue("@BudgetID", GetBudgetId());
                    insertCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void InsertNeedDetails()
        {
            if (Needs != null && Needs.Count-1 > 0)
            {
                foreach (var need in Needs)
                {
                    string insertNeedDetails = "INSERT INTO dbo.NeedDetails (NeedID, NeedName, NeedValue) VALUES (@NeedID, @NeedName, 0)";

                    using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand insertCommand = new SqlCommand(insertNeedDetails, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@NeedID", GetNeedId());
                            insertCommand.Parameters.AddWithValue("@NeedName", need);
                            insertCommand.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
            }
        }

        public int GetNeedId()
        {
            string getNeedIdQuery = "SELECT MAX(NeedID) FROM dbo.Need";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();

                using (SqlCommand getCommand = new SqlCommand(getNeedIdQuery, connection))
                {
                    int needId = (int)getCommand.ExecuteScalar();
                    return needId;
                }
            }
        }
        public int GetBudgetId()
        {
            string GetBudgetId = "SELECT BudgetID FROM [dbo].[Budget] WHERE CustomerId like '" + getCustomerID() + "'";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();

                using (SqlCommand SelectCustomerID = new SqlCommand(GetBudgetId, connection))
                {
                    using (SqlDataReader reader = SelectCustomerID.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int BID = reader.GetInt32(0);
                            return BID;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }

            } 
        }
    }
}
