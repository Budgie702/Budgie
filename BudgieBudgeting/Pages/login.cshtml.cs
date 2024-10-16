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
            if (Credential == null)
            {
                ErrorMessage = "Credential is null";
                return;
            }

            if (ModelState.IsValid)
            {
                string query = "SELECT Email, UserPassword FROM Customer WHERE Email = @Email";

                using (SqlConnection connection = new SqlConnection(DatabaseConnection.Connection.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Credential.Email .ToLower());

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string email = reader.GetString(0);
                                string password = reader.GetString(1);

                                if (Credential.Password == password)
                                {
                                    if (Response != null)
                                    {
                                        Response.Redirect("/Homepage");
                                    }
                                    else
                                    {
                                        ErrorMessage = "Response object is null";
                                    }
                                   
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
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
