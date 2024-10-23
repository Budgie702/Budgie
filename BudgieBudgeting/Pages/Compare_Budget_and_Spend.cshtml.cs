using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // For session access

namespace BudgieBudgeting.Pages
{
    public class Compare_Budget_and_SpendModel : PageModel
    {
        public string? Username { get; private set; } // Property to hold the username

        public void OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username");

            // Optional: Check if the username is null or empty
            if (string.IsNullOrEmpty(Username))
            {
                // Handle the case where the username is not found in the session
                // Redirect to login or display an error message
                RedirectToPage("/Login");
            }

            // Here you can perform any additional logic needed for the page
        }
    }
}
