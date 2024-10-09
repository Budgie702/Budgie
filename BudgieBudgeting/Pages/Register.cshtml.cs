using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BudgieBudgeting.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public required RegisterCredential RegisterCredential { get; set; }

        public required string ErrorMessage { get; set; }

        public void OnPost()
        {
            Response.Redirect("/homepage");
        }
    }
    public class RegisterCredential
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
