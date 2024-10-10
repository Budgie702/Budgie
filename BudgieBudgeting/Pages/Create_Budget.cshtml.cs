using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public void OnGet()
        {
            // Initialization logic, if needed
        }

        public IActionResult OnPost()
        {
            // Process the form submission, save to the database
            return RedirectToPage("/Success");
        }
    }
}
