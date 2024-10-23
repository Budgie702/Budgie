using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http; // For session access

namespace BudgieBudgeting.Pages
{
    public class Check_SpendingModel : PageModel
    {
        public List<string> Needs { get; set; } = new List<string>();
        public List<string> Wants { get; set; } = new List<string>();
        public List<string> Savings { get; set; } = new List<string>();

        public string? Username { get; private set; } // Property to hold the username

        public void OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username");

            // Optional: Check if the username is null or empty
            if (string.IsNullOrEmpty(Username))
            {
                // Handle the case where the username is not found in the session
                // For example, redirect to the login page
                RedirectToPage("/Login");
            }

            // Here you can also retrieve Needs, Wants, and Savings from the database when you're ready
        }
    }
}
