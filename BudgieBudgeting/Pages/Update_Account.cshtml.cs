using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for accessing session

namespace BudgieBudgeting.Pages
{
    public class Update_AccountModel : PageModel
    {
        // Properties to hold user account details
        public required string? Email { get; set; }
        public required string? Username { get; set; }
        public required string? Password { get; set; } // Consider using a more secure approach for handling passwords

        public void OnGet()
        {
            // Retrieve the username from the session
            Username = HttpContext.Session.GetString("Username"); // Use session to retrieve username

            // TODO: Retrieve user data from your database using the Username
            /*
            using (var context = new YourDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Username == Username);
                if (user != null)
                {
                    Email = user.Email;
                    Username = user.Username;
                    // Password should not be retrieved directly; handle securely
                }
            }
            */
        }

        public IActionResult OnPost(string email, string username, string password)
        {
            // TODO: Handle form submission to update the user account
            // Validate and save the updated information to your database
            /*
            using (var context = new YourDbContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user != null)
                {
                    user.Email = email;
                    // Consider updating password securely, e.g., hashing it
                    await context.SaveChangesAsync();
                }
            }
            */

            return RedirectToPage(); // Redirect after successful update
        }
    }
}
