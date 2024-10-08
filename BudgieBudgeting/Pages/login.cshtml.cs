using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BudgieBudgeting.Pages
{
    public class loginModel : PageModel
    {
   
    

        public void OnPost()
        {
            Response.Redirect("/Homepage");
        }


    }
    public class Credential
    {
      
      
    }
}
