using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BudgieBudgeting.Pages
{
	public class loginModel : PageModel
	{
		[BindProperty]
		public required Credential Credential { get; set; }

		public required string ErrorMessage { get; set; }

		public void OnPost()
		{
			if (ModelState.IsValid)
			{
				if (Credential.Username == "admin" && Credential.Password == "admin")
				{
					Response.Redirect("/Homepage");
				}
				else
				{
					ErrorMessage = "Invalid username or password";
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
