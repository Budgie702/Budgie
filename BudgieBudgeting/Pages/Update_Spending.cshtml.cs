using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Ensure this using directive is present
using System.Collections.Generic;

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
        public List<decimal> NeedsAmount { get; set; } = new List<decimal>();

        [BindProperty]
        public List<decimal> WantsAmount { get; set; } = new List<decimal>();

        [BindProperty]
        public List<decimal> SavingsAmount { get; set; } = new List<decimal>();

        public string Username { get; private set; } // Property to hold the username

        public IActionResult OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username"); // Ensure the casing matches

            // Optional: Check if the username is null or empty and handle it
            if (string.IsNullOrEmpty(Username))
            {
                // Redirect to the login page if the username is not found
                return RedirectToPage("/Login");
            }

            // Load data from the database here if needed
            return Page(); // Return the page to render
        }

        public IActionResult OnPost()
        {
            // Process form submission here (e.g., save to the database)
            return RedirectToPage("/Success");
        }
    }
}
