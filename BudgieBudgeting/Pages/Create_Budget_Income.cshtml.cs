using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BudgieBudgeting.Pages
{
    public class Create_Budget_IncomeModel : PageModel
    {
        [BindProperty]
        public required string BudgetAmount { get; set; }

        public string? ErrorMessage { get; set; }
        public string? Username { get; private set; } // Property to hold the username from session

        private readonly DatabaseConnection _databaseConnection;

        public Create_Budget_IncomeModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IActionResult OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username");

            // Optional: Check if username is null or empty and handle it
            if (string.IsNullOrEmpty(Username))
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(Username))
            {
                // If the user is not logged in, redirect to the login page
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrEmpty(BudgetAmount))
            {
                ErrorMessage = "Budget amount cannot be empty.";
                return Page(); // Return the page to show the error
            }

            // SQL query to update the income for the user based on username
            string updateIncomeQuery = "UPDATE dbo.Customer SET Income = @Income WHERE Username = @Username";

            using (SqlConnection connection = _databaseConnection.Connection)
            {
                connection.Open();

                using (SqlCommand updateCommand = new SqlCommand(updateIncomeQuery, connection))
                {
                    // Add parameters for income and username
                    updateCommand.Parameters.AddWithValue("@Income", BudgetAmount);
                    updateCommand.Parameters.AddWithValue("@Username", Username);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        ErrorMessage = "Error: No customer found with the provided username.";
                        return Page();
                    }
                }
                connection.Close();
            }

            // After updating the income, redirect to a success page or any other desired page
            return RedirectToPage("/Create_Budget");
        }
    }
}