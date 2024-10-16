using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http; // Ensure this is included for session handling

namespace BudgieBudgeting.Pages.Shared
{
    public class loginModel : PageModel // Renamed to follow C# conventions (PascalCase)
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential(); // Initialize to avoid null reference

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // This method is intentionally left empty; it can be used to prepopulate fields if needed.
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string query = "SELECT Username, UserPassword FROM Customer WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(DatabaseConnection.Connection.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Credential.Email); // Use Email for the query

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string username = reader.GetString(0); // Get the username
                                string password = reader.GetString(1);

                                if (Credential.Password == password) // Compare passwords
                                {
                                    // Store the username in the session
                                    HttpContext.Session.SetString("Username", username);

                                    // Redirect to the homepage
                                    return RedirectToPage("/Homepage"); // Use RedirectToPage instead of Response.Redirect
                                }
                                else
                                {
                                    ErrorMessage = "Invalid password"; // Set error message for invalid password
                                }
                            }
                            else
                            {
                                ErrorMessage = "Invalid email"; // Set error message for invalid email
                            }
                        }
                    }
                }
            }
            return Page(); // Return to the same page if validation fails or an error occurs
        }
    }

    public class Credential
    {
        [Required]
        [EmailAddress] // Email format validation
        public string Email { get; set; } = string.Empty; // Changed Username to Email

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty; // Initialize to avoid null reference
    }
}
