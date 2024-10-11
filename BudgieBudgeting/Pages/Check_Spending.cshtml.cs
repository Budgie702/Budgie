using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BudgieBudgeting.Pages
{
    public class Check_SpendingModel : PageModel
    {
        public List<string> Needs { get; set; } = new List<string>();
        public List<string> Wants { get; set; } = new List<string>();
        public List<string> Savings { get; set; } = new List<string>();

        public void OnGet()
        {
            // Retrieve Needs, Wants, and Savings from the database
            // Example:
            // Needs = _yourDatabaseService.GetNeeds();
            // Wants = _yourDatabaseService.GetWants();
            // Savings = _yourDatabaseService.GetSavings();
        }
    }
}
