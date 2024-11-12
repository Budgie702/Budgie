using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Ensure this using directive is present
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BudgieBudgeting.Pages
{
    public class Update_SpendingModel : PageModel
    {
        [BindProperty]
        public List<string> Needs { get; set; } = new List<string>();

        [BindProperty]
        public List<string> Wants { get; set; } = new List<string>();

        [BindProperty]
        public List<string> Savings { get; set; } = new List<string>();

        [BindProperty]
        public List<string> NeedDetails { get; set; } = new List<string>();
        [BindProperty]
        public List<string> WantDetails { get; set; } = new List<string>();
        [BindProperty]
        public List<string> SavingsDetails { get; set; } = new List<string>();


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
        decimal needsMultiplier = 0.5m;
        decimal wantsMultiplier = 0.3m;
        decimal savingsMultiplier = 0.2m;

        private readonly DatabaseConnection _databaseConnection;

        public Update_SpendingModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(Username))
            {
                RedirectToPage("/Login");
            }
            Username = HttpContext.Session.GetString("Username");


        }

        public IActionResult OnPost()
        {
            InsertBudget();
            InsertNeed();
            InsertNeedDetails();
            InsertWant();
            InsertWantDetails();
            InsertSavings();
            InsertSavingsDetails();
            return RedirectToPage("/Homepage");
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


        public void InsertBudget()
        {
            decimal income = getCustomerIncome();
            decimal needsBudget = income * needsMultiplier;
            decimal wantsBudget = income * wantsMultiplier;
            decimal savingsBudget = income * savingsMultiplier;

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
            if (Needs != null && Needs.Count - 1 > 0)
            {
                for (int i = 0; i < Needs.Count; i++)
                {
                    string insertNeedDetails = "INSERT INTO dbo.NeedDetails (NeedID, NeedName, NeedValue) VALUES (@NeedID, @NeedName, @NeedValue)";

                    using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand insertCommand = new SqlCommand(insertNeedDetails, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@NeedID", GetNeedId());
                            insertCommand.Parameters.AddWithValue("@NeedName", Needs[i]);
                            insertCommand.Parameters.AddWithValue("@NeedValue", NeedDetails[i]);
                            insertCommand.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
            }
        }

        public void InsertWant()
        {
            string insertNeedTable = "INSERT INTO dbo.Wants(BudgetID) VALUES (@BudgetID)";
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
        public void InsertWantDetails()
        {
            if (Wants != null && Wants.Count - 1 > 0)
            {
                for (int i = 0; i < Wants.Count; i++)
                {
                    string insertNeedDetails = "INSERT INTO dbo.WantsDetails (WantsID, WantName, WantsValue) VALUES (@NeedID, @NeedName, @WantsValue)";

                    using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand insertCommand = new SqlCommand(insertNeedDetails, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@NeedID", GetWantsId());
                            insertCommand.Parameters.AddWithValue("@NeedName", Wants[i]);
                            insertCommand.Parameters.AddWithValue("@WantsValue", WantDetails[i]);
                            insertCommand.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
            }
        }
        public void InsertSavings()
        {
            string insertNeedTable = "INSERT INTO dbo.Savings (BudgetID) VALUES (@BudgetID)";
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
        public void InsertSavingsDetails()
        {
            if (Needs != null && Needs.Count - 1 > 0)
            {
                for (int i = 0; i < Savings.Count; i++)
                {
                    string insertNeedDetails = "INSERT INTO dbo.SavingsDetails (SavingsID, SavingName, SavingsValue) VALUES (@NeedID, @NeedName, 0)";

                    using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand insertCommand = new SqlCommand(insertNeedDetails, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@NeedID", GetSavingsId());
                            insertCommand.Parameters.AddWithValue("@NeedName", Savings[i]);
                            insertCommand.Parameters.AddWithValue("@SavingsValue", SavingsDetails[i]);
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
        public int GetWantsId()
        {
            string getNeedIdQuery = "SELECT MAX(WantsID) FROM dbo.Wants";

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
        public int GetSavingsId()
        {
            string getNeedIdQuery = "SELECT MAX(SavingsID) FROM dbo.Savings";

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
