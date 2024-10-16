using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace BudgieBudgeting.Pages.Shared
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public required Credential Credential { get; set; }

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                string query = "SELECT Username, UserPassword FROM Customer WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(DatabaseConnection.Connection.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Credential.Username); // Use Username for email

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string username = reader.GetString(0); // Get the username
                                string password = reader.GetString(1);

                                if (Credential.Password == password)
                                {
                                    // Store the username in the session
                                    HttpContext.Session.SetString("Username", username);

                                    // Redirect to the homepage
                                    Response.Redirect("/Homepage");
                                }
                                else
                                {
                                    ErrorMessage = "Invalid password";
                                }
                            }
                            else
                            {
                                ErrorMessage = "Invalid Email";
                            }
                        }
                    }
                }
            }
        }
    }

    public class Credential
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
