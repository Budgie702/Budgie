using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Ensure you have this using directive

namespace BudgieBudgeting.Pages.Shared
{
    public class HomepageModel : PageModel
    {
        public string Username { get; private set; } // Property to hold the username

        public IActionResult OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username"); 

            // Optional: Check if username is null or empty and handle it
            if (string.IsNullOrEmpty(Username))
            {
                // Redirect to the login page if the username is not found
                return RedirectToPage("/Login");
            }

            return Page(); // Return the page if the username is valid 

            
        }
    }
}
