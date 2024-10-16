using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for accessing sessions

namespace BudgieBudgeting.Pages
{
    public class Create_Budget_IncomeModel : PageModel
    {
        [BindProperty]
        public string BudgetAmount { get; set; }

        public string ErrorMessage { get; set; }
        public string Username { get; private set; } // Property to hold the username

        public void OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username");

            // Optional: Check if username is null or empty
            if (string.IsNullOrEmpty(Username))
            {
                // Handle the case where the username is not found in the session
                // For example, redirect to the login page
                RedirectToPage("/Login");
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(BudgetAmount))
            {
                ErrorMessage = "Budget amount cannot be empty.";
                return Page(); // Return the page to show the error
            }
            else
            {
                // Handle form submission logic here, like saving the budget to the database
                // You can use the Username variable for your database logic
                ErrorMessage = null;

                // Redirect to a success page or another page after handling the budget
                return RedirectToPage("/Success");
            }
        }
    }
}
