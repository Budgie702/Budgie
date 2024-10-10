using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgieBudgeting.Pages
{
    public class Create_BudgetModel : PageModel
    {
        [BindProperty]
        public string BudgetAmount { get; set; }

        public string ErrorMessage { get; set; }

        public void OnPost()
        {
            if (string.IsNullOrEmpty(BudgetAmount))
            {
                ErrorMessage = "Budget amount cannot be empty.";
            }
            else
            {
                // Handle form submission logic here, like saving the budget to the database
                ErrorMessage = null;
            }
        }
    }
}