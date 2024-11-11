using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace BudgieBudgeting.Pages
{
    public class Delete_AccountModel : PageModel
    {
        [BindProperty]
        public required DeleteCredential DeleteCredential { get; set; }

        public required string ErrorMessage { get; set; }

        private readonly DatabaseConnection _databaseConnection;

        public Delete_AccountModel(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public virtual void OnPost()
        {
            string DeleteQuery = "DELETE FROM dbo.Customer WHERE email = @Email AND UserPassword = @UserPassword";
            int rowsDeleted = 0;

            using (SqlConnection connection = _databaseConnection.Connection)
            {
                connection.Open();

                using (SqlCommand deleteCommand = new SqlCommand(DeleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@Email", DeleteCredential.Email);
                    deleteCommand.Parameters.AddWithValue("@UserPassword", DeleteCredential.Password);

                    rowsDeleted = deleteCommand.ExecuteNonQuery();
                }
            }

            if (rowsDeleted > 0)
            {
                try
                {
                    Response.Redirect("/Homepage");
                }
                catch
                {

                }

            }
            else
            {
                ErrorMessage = "Account Not Found";
                return;
            }
        }
    }

    public class DeleteCredential
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
