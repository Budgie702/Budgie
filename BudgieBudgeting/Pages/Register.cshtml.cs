using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
namespace BudgieBudgeting.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public required RegisterCredential RegisterCredential { get; set; }

        public required string ErrorMessage { get; set; }

        public void OnPost()
        {
            string sql = "INSERT INTO dbo.Customer (Username, email, UserPassword) VALUES (@Username, @email, @UserPassword);";
            using (SqlConnection connection = DatabaseConnection.Connection)
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Username", RegisterCredential.Username);
                command.Parameters.AddWithValue("@email", RegisterCredential.Email);
                command.Parameters.AddWithValue("@UserPassword", RegisterCredential.Password);
                connection.Open();
                command.ExecuteNonQuery();
            }
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
