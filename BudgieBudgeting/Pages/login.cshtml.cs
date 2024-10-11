using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient; // Updated namespace
using System.Net;

namespace BudgieBudgeting.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public required Credential Credential { get; set; }

        public string? ErrorMessage { get; set; } // Made nullable

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Credential.Username == "admin" && Credential.Password == "admin")
                {
                    Response.Redirect("/Homepage");
                }
                /*else
                {   
                    ErrorMessage = "Invalid username or password";
                }*/
                else
                {
                    try
                    {
                        string connectionString = "Server=tcp:budgie-budgeting.database.windows.net,1433;Initial Catalog=Budgie;Persist Security Info=False;User ID=Budgie;Password=Budgeting12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            ErrorMessage = "Database connection successful";
                        }
                    }
                    catch (SqlException)
                    {
                        ErrorMessage = "Database connection failed. Please try again later.";
                    }
                    catch (Exception)
                    {
                        ErrorMessage = "An unexpected error occurred. Please try again later.";
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
