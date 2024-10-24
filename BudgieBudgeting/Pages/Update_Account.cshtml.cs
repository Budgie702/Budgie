using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.ComponentModel.DataAnnotations;

namespace BudgieBudgeting.Pages
{
    public class Update_AccountModel : PageModel
    {

        private readonly DatabaseConnection _databaseConnection;

        public Update_AccountModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        // Properties to hold user account details
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; } // Consider using a more secure approach for handling passwords

        public string? ErrorMessage { get; set; }

        public virtual void OnGet()
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

        public IActionResult OnPost(UpdateCredential credential)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                string query = "UPDATE dbo.Customer SET Email = @Email, Username = @Username, UserPassword = @Password WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", credential.Email);
                    command.Parameters.AddWithValue("@Username", credential.Username);
                    command.Parameters.AddWithValue("@Password", credential.Password);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return RedirectToPage("Homepage");
                    }
                    else
                    {
                        ErrorMessage = "Invalid password";
                    }
                }
            }

            ErrorMessage = "Invalid email";
            return Page();
        }
    }
    public class UpdateCredential
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

