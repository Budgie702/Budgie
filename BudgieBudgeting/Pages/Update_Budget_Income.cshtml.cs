using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Ensure this using directive is present

namespace BudgieBudgeting.Pages
{
    public class Update_Budget_IncomeModel : PageModel
    {
        [BindProperty]
        public string BudgetAmount { get; set; }

        public string ErrorMessage { get; set; }

        public string Username { get; private set; } // Property to hold the username

        public void OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username"); // Ensure the casing matches

            // Optional: Check if the username is null or empty and handle it
            if (string.IsNullOrEmpty(Username))
            {
                // Redirect to the login page if the username is not found
                RedirectToPage("/Login");
            }
        }

        public void OnPost()
        {
            if (string.IsNullOrEmpty(BudgetAmount))
            {
                ErrorMessage = "Budget amount cannot be empty.";
            }
            else
            {
                // Handle form submission logic here, like saving the budget to the database
                ErrorMessage = null;
                // Use the Username property if needed for logging or displaying messages
            }
        }
    }
}
