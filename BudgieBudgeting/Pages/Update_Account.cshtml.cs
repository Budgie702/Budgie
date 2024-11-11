using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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

        public string? ErrorMessage { get; set; }

        // Make updateCredential public so it's accessible in the Razor Page
        [BindProperty]
        public UpdateCredential updateCredential { get; set; } = new UpdateCredential();

        public virtual void OnGet()
        {
            // Retrieve the username from the session
            string Username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(Username))
            {
                ErrorMessage = "No user logged in.";
                return;
            }

            string query = "SELECT CustomerId, Email, UserPassword FROM dbo.Customer WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            updateCredential.Username = Username;
                            updateCredential.CustomerId = reader.GetInt32(0);
                            updateCredential.Email = reader.GetString(1);
                            updateCredential.Password = reader.GetString(2);
                            Console.WriteLine($"Email: {updateCredential.Email}");
                            Console.WriteLine($"Username: {updateCredential.Username}");
                            Console.WriteLine($"Password: {updateCredential.Password}");
                            Console.WriteLine($"CustomerId: {updateCredential.CustomerId}");
                        }
                        else
                        {
                            ErrorMessage = "User data not found.";
                        }
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                string query = "UPDATE dbo.Customer SET Email = @Email, Username = @Username, UserPassword = @Password WHERE CustomerId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", updateCredential.Email);
                    command.Parameters.AddWithValue("@Username", updateCredential.Username);
                    command.Parameters.AddWithValue("@Password", updateCredential.Password);
                    command.Parameters.AddWithValue("@Id", updateCredential.CustomerId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        HttpContext.Session.Clear();


                        return Content(@"<script>
                                    alert('Update successful! You will now be redirected to the login page.');
                                    window.location.href = '/Login';
                                 </script>", "text/html");
                    }
                    else
                    {
                        ErrorMessage = "Update failed. Please try again.";
                    }
                }
            }

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

        [Required]
        public int CustomerId { get; set; }
    }
}
