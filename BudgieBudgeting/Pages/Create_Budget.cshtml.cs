using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for accessing sessions
using System.Collections.Generic;

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

        public string? Username { get; private set; } // Property to hold the username

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
            // Save the Needs, Wants, and Savings to the database here (not implemented yet)

            // Redirect to a success page or another page
            return RedirectToPage("/Success");
        }
    }
}
