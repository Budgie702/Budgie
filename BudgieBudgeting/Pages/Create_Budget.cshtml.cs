using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for accessing sessions
using System.Collections.Generic;
using BudgieBudgeting.DatabaseItems;
using Microsoft.Data.SqlClient;
using BudgieBudgeting.Pages.Shared;
using Castle.Core.Resource;

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
                // Handle the case where the username is not found in the session
                // Redirect to login or display an error message
                RedirectToPage("/Login");
            }
        }

        public IActionResult OnPost()
        {
            // Process the form submission 

            //Saving Info for Budget Table 
                //check database for highest Budget ID and +1 
                //find CustomerID linked to Username 
                //take Income from Customers table linked to Username 
                //Divide income by 50%, 30% and 20% and save to NeedsBudget, WantsBudget, SavingsBudget 

                BudgetID = getBudgetID() + 1;
                CustomerID = HttpContext.Session.GetString("CustomerId");
                Income = getCustomerIncome();
                NeedsBudget = (int)Income * .5;
                WantsBudget = (int)Income * .3;
                SavingsBudget = (int)Income * .2;

                string insertQuery = "INSERT INTO dbo.Budget (BudgetId, CustomerId, NeedsBudget, WantsBudget, SavingsBudget) VALUES (BudgetID, CustomerID, NeedsBudget, WantsBudget, SavingsBudget )";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("BudgetID", BudgetID);
                        insertCommand.Parameters.AddWithValue("CustomerID", CustomerID);
                        insertCommand.Parameters.AddWithValue("NeedsBudget", NeedsBudget);
                        insertCommand.Parameters.AddWithValue("WantsBudget", WantsBudget);
                        insertCommand.Parameters.AddWithValue("SavingsBudget", SavingsBudget);
                        insertCommand.ExecuteNonQuery();
                    }
                }


            //Saving Info for Need Table 
                //take current Budget ID and save 
                //check database for highest NeedID and +1 

                NeedID = getNeedsID() + 1;

                string insertNeedTable = "INSERT INTO dbo.Need (BudgetId, NeedId) VALUES (BudgetID, NeedID )";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();

                    using (SqlCommand insertCommand = new SqlCommand(insertNeedTable, connection))
                    {
                        insertCommand.Parameters.AddWithValue("BudgetID", BudgetID);
                        insertCommand.Parameters.AddWithValue("NeedID", NeedID);
                        insertCommand.ExecuteNonQuery();
                    }
                }


            //Saving Info for Want Table 
            //take current Budget ID and save 
            //check database for highest WantID and +1 

            WantID = getWantsID() + 1;


            //Saving Info for Savings Table 
                //take current Budget ID and save 
                //check database for highest SavingsID and +1 

            SavingID = getSavingsID() + 1;


            // Save the Needs to the NeedsDetails table 
                // Loop through Needs[] to take each item 

                foreach(var need in Needs)
                {
                    // Find the highest NeedDetailsID and +1 
                    // Take current NeedID and save 
                    // Take name value and save 
                    // need value remains blank for current time 

                    NeedDetailsID = getNeedDetailsID() + 1;

                    string insertNeedDetails = "INSERT INTO dbo.NeedDetails (NeedDetailsID, NeedID, NeedName, NeedValue) VALUES (NeedDetailsID, NeedID, need, 0)";

                using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
                {
                    connection.Open();

                        using (SqlCommand insertCommand = new SqlCommand(insertNeedDetails, connection))
                        {
                            insertCommand.Parameters.AddWithValue("NeedDetailsID", NeedDetailsID);
                            insertCommand.Parameters.AddWithValue("NeedID", NeedID);
                            insertCommand.Parameters.AddWithValue("need", need);
                            insertCommand.Parameters.AddWithValue("0", 0);
                            insertCommand.ExecuteNonQuery();
                        }
                    }

            }


            // Redirect to a success page or another page 

            return RedirectToPage("/Success");
        }

        public int getBudgetID()
        {
            // SQL query to get the max BudgetID 
            string getMaxBudgetIDQuery = "SELECT MAX(BudgetID) FROM dbo.Budget";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getMaxBudgetIDQuery, connection))
                {
                    // Add parameters for income and username 
                    getCommand.Parameters.AddWithValue("@BudgetID", BudgetID);
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No BudgetID was found.";
                        return -1;
                    }

                    else
                    {
                        return @BudgetID;
                    }
                }
            }
        }

        public int getNeedsID()
        {
            // SQL query to get the max NeedID 
            string getMaxNeedIDQuery = "SELECT MAX(NeedID) FROM dbo.Need";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getMaxNeedIDQuery, connection))
                {
                    // Add parameters for income and username 
                    getCommand.Parameters.AddWithValue("@NeedID", NeedID);
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No NeedID was found.";
                        return -1;
                    }

                    else
                    {
                        return @NeedID;
                    }
                }
            }
        }

        public int getWantsID()
        {
            // SQL query to get the max NeedID 
            string getMaxWantIDQuery = "SELECT MAX(WantID) FROM dbo.Want";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getMaxWantIDQuery, connection))
                {
                    // Add parameters for income and username 
                    getCommand.Parameters.AddWithValue("@WantID", WantID);
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No WantID was found.";
                        return -1;
                    }

                    else
                    {
                        return @WantID;
                    }
                }
            }
        }

        public int getSavingsID()
        {
            // SQL query to get the max NeedID 
            string getMaxSavingIDQuery = "SELECT MAX(SavingID) FROM dbo.Saving";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getMaxSavingIDQuery, connection))
                {
                    // Add parameters for income and username 
                    getCommand.Parameters.AddWithValue("@SavingID", SavingID);
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No SavingID was found.";
                        return -1;
                    }

                    else
                    {
                        return @SavingID;
                    }
                }
            }
        }

        public int getCustomerIncome()
        {
            // SQL query to get the max NeedID 
            string getCustomerIncomeQuery = "SELECT Income FROM dbo.Customer Where Username like '" + Username + "'";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
  
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getCustomerIncomeQuery, connection))
                {
                    
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No SavingID was found.";
                        return -1;
                    }

                    else
                    {
                        return (int)@Income;
                    }
                }
            }
        }

        public int getCustomerID()
        {
            // SQL query to get the CustomerID 
            string getCustomerIncomeQuery = "SELECT CustomerId FROM dbo.Customer where USERNAME = Username";

            using (SqlConnection connection = _databaseConnection.Connection)
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getCustomerIncomeQuery, connection))
                {
                    // Add parameters for income and username 
                    getCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No CustomerID was found.";
                        return -1;
                    }

                    else
                    {
                        return (int)@CustomerID;
                    }
                }
            }
        }
            public int getNeedDetailsID()
        {
            // SQL query to get the max NeedDetailID
            string getMaxNeedDetailsIDQuery = "SELECT MAX(NeedDetailID) FROM dbo.NeedDetails";

            using (SqlConnection connection = _databaseConnection.Connection)
            {
                connection.Open();
                using (SqlCommand getCommand = new SqlCommand(getMaxNeedDetailsIDQuery, connection))
                {
                    // Add parameters for income and username 
                    getCommand.Parameters.AddWithValue("@NeedDetailID", NeedDetailID);
                    int rowsAffected = getCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No NeedDetailID was found.";
                        return -1;
                    }

                    else
                    {
                        return @NeedDetailID;
                    }
                }
            }
        }


    }
}
