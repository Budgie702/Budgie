using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.ComponentModel.DataAnnotations;
using System.Data;

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
        UpdateCredential updateCredential { get; set; }

        public virtual void OnGet()
        {
            // Retrieve the username from the session
            String Username = HttpContext.Session.GetString("Username"); // Use session to retrieve username

            string query = "Select CustomerId,Email,Password form dbo.Customer where @Username = Username";
            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    connection.Close();
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    updateCredential.Username = Username;
                    updateCredential.CustomerId = Convert.ToInt32(table.Rows[0][0]);
                    updateCredential.Email = table.Rows[0][1].ToString();
                    updateCredential.Password = table.Rows[0][2].ToString();
                }
            }
        }

        public IActionResult OnPost(UpdateCredential credential)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(_databaseConnection.Connection.ConnectionString))
            {
                string query = "UPDATE dbo.Customer SET Email = @Email, Username = @Username, UserPassword = @Password WHERE @Id = CustomerId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", credential.Email);
                    command.Parameters.AddWithValue("@Username", credential.Username);
                    command.Parameters.AddWithValue("@Password", credential.Password);
                    command.Parameters.AddWithValue("@Id",credential.CustomerId);

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
        [Required]
        public int CustomerId {  get; set; }
    }
}

