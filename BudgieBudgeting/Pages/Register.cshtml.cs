using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
namespace BudgieBudgeting.Pages.Shared
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public required RegisterCredential RegisterCredential { get; set; }

        public required string ErrorMessage { get; set; }
        private readonly DatabaseConnection _databaseConnection;

        public RegisterModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public virtual void OnPost()
        {
            string checkEmailQuery = "SELECT COUNT(*) FROM dbo.Customer WHERE email = @Email";
            string insertQuery = "INSERT INTO dbo.Customer (Username, email, UserPassword) VALUES (@Username, @Email, @UserPassword)";

            using (SqlConnection connection = _databaseConnection.Connection)
            {
                connection.Open();

                using (SqlCommand checkCommand = new SqlCommand(checkEmailQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Email", RegisterCredential.Email);
                    int emailCount = (int)checkCommand.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        ErrorMessage = "Email already exists.";
                        return;
                    }
                }

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Username", RegisterCredential.Username);
                    insertCommand.Parameters.AddWithValue("@Email", RegisterCredential.Email.ToLower());
                    insertCommand.Parameters.AddWithValue("@UserPassword", RegisterCredential.Password);
                    insertCommand.ExecuteNonQuery();
                }
            }

            Response.Redirect("/Homepage");
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
