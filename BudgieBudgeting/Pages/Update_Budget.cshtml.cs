using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        // This method runs when the page is loaded
        public async Task OnGetAsync()
        {
            // TODO: Connect to your database here and retrieve data for Needs, Wants, and Savings

            // Example connection to the database (pseudo-code):
            /*
            using (var context = new YourDbContext())
            {
                Needs = await context.BudgetItems
                    .Where(item => item.Type == "Need")
                    .Select(item => item.Description)
                    .ToListAsync();

                Wants = await context.BudgetItems
                    .Where(item => item.Type == "Want")
                    .Select(item => item.Description)
                    .ToListAsync();

                Savings = await context.BudgetItems
                    .Where(item => item.Type == "Saving")
                    .Select(item => item.Description)
                    .ToListAsync();
            }
            */
        }

        // This method handles the form submission when the update button is clicked
        public async Task<IActionResult> OnPostAsync(string[] Needs, string[] Wants, string[] Savings)
        {
            // TODO: Connect to your database here to update the Needs, Wants, and Savings

            // Example connection to the database (pseudo-code):
            /*
            using (var context = new YourDbContext())
            {
                // Clear existing items
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Need"));
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Want"));
                context.BudgetItems.RemoveRange(context.BudgetItems.Where(item => item.Type == "Saving"));

                // Add new items
                foreach (var need in Needs)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Need", Description = need });
                }
                foreach (var want in Wants)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Want", Description = want });
                }
                foreach (var saving in Savings)
                {
                    context.BudgetItems.Add(new BudgetItem { Type = "Saving", Description = saving });
                }

                await context.SaveChangesAsync(); // Save changes to the database
            }
            */

            return RedirectToPage(); // Redirect back to the same page
        }
    }
}
