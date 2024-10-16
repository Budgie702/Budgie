using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient; // Updated namespace
using System.Net;

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
                string query = "SELECT Email, UserPassword FROM Customer WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(DatabaseConnection.Connection.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Credential.Username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string email = reader.GetString(0);
                                string password = reader.GetString(1);

                                if (Credential.Password == password)
                                {
                                    Response.Redirect("/Homepage");
                                }
                                else
                                {
                                    ErrorMessage = "Invalid password";
                                }
                            }
                            else
                            {
                                ErrorMessage = "Invalid username";
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
