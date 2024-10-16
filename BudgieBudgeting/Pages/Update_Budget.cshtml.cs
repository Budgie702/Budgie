using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for accessing cookies
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgieBudgeting.Pages
{
    public class Update_BudgetModel : PageModel
    {
        // Properties to hold data for Needs, Wants, and Savings
        public List<string> Needs { get; set; } = new List<string>();
        public List<string> Wants { get; set; } = new List<string>();
        public List<string> Savings { get; set; } = new List<string>();

        public string Username { get; private set; } // Property to hold the username

        // This method runs when the page is loaded
        public async Task OnGetAsync()
        {
            // Retrieve the username from the cookie
            Username = HttpContext.Session.GetString("Username"); // Use the same key you used when setting the session

            // TODO: Connect to your database here and retrieve data for Needs, Wants, and Savings
            // You can use the Username variable for your queries
            /*
            using (var context = new YourDbContext())
            {
                Needs = await context.BudgetItems
                    .Where(item => item.Type == "Need" && item.Username == Username) // Adjust query to include username
                    .Select(item => item.Description)
                    .ToListAsync();

                Wants = await context.BudgetItems
                    .Where(item => item.Type == "Want" && item.Username == Username) // Adjust query to include username
                    .Select(item => item.Description)
                    .ToListAsync();

                Savings = await context.BudgetItems
                    .Where(item => item.Type == "Saving" && item.Username == Username) // Adjust query to include username
                    .Select(item => item.Description)
                    .ToListAsync();
            }
            */
        }

        // This method handles the form submission when the update button is clicked
        public async Task<IActionResult> OnPostAsync(string[] Needs, string[] Wants, string[] Savings)
        {
            // TODO: Connect to your database here to update the Needs, Wants, and Savings
            // You can use the Username variable for your queries
            /*
            using (var context = new YourDbContext())
            {
                // Clear existing items for the user
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Need" && item.Username == Username));
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Want" && item.Username == Username));
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Saving" && item.Username == Username));

                // Add new items for the user
                foreach (var need in Needs)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Need", Description = need, Username = Username });
                }
                foreach (var want in Wants)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Want", Description = want, Username = Username });
                }
                foreach (var saving in Savings)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Saving", Description = saving, Username = Username });
                }

                await context.SaveChangesAsync(); // Save changes to the database
            }
            */

            return RedirectToPage(); // Redirect back to the same page
        }
    }
}
