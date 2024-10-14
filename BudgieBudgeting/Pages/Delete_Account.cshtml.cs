using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace BudgieBudgeting.Pages
{
	public class Delete_AccountModel : PageModel
	{
		[BindProperty]
		public DeleteCredential DeleteCredential { get; set; }

		public string ErrorMessage { get; set; }

		public void OnPost()
		{
			string DeleteQuery = "DELETE FROM dbo.Customer WHERE email = @Email AND UserPassword = @UserPassword";

			using (SqlConnection connection = DatabaseConnection.Connection)
			{
				connection.Open();

				using (SqlCommand insertCommand = new SqlCommand(DeleteQuery, connection))
				{
					insertCommand.Parameters.AddWithValue("@Email", DeleteCredential.Email);
					insertCommand.Parameters.AddWithValue("@UserPassword", DeleteCredential.Password);

					insertCommand.ExecuteNonQuery();
				}
			}

			Response.Redirect("/Homepage");
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
