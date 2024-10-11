using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public void OnGet()
        {
            // Load data from the database here
        }

        public IActionResult OnPost()
        {
            // Process form submission here (e.g., save to the database)
            return RedirectToPage("/Success");
        }
    }
}
