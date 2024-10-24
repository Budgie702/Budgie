using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace BudgieBudgeting.Pages.Shared
{
    public class loginModel : PageModel
    {
        private readonly DatabaseConnection _databaseConnection;

        public loginModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        [BindProperty]
        public Credential Credential { get; set; } = new Credential();

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

                Console.WriteLine(_databaseConnection.Connection.ConnectionString);
                using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Credential.Email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string username = reader.GetString(0);
                                string password = reader.GetString(1);

                                if (Credential.Password == password)
                                {
                                    HttpContext.Session.SetString("Username", username);
                                    return RedirectToPage("/Homepage");
                                }
                                else
                                {
                                    ErrorMessage = "Invalid password";
                                }
                            }
                            else
                            {
                                ErrorMessage = "Invalid email";
                            }
                        }
                    }
                }
            }
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}