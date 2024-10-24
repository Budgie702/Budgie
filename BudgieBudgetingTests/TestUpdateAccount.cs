namespace BudgieBudgeting;
using Moq;
using Microsoft.AspNetCore.Http;
using BudgieBudgeting.Pages;

[TestClass]
public class TestUpdate
{
    [TestMethod]
    public void TestOnGet()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Session = new Mock<ISession>().Object;
        var updateAccount = new Update_AccountModel
        {
            PageContext = new Microsoft.AspNetCore.Mvc.RazorPages.PageContext
            {
                HttpContext = httpContext
            },
            Email = "test@example.com",
            Username = "testuser",
            Password = "password123"
        };
    }
    [TestMethod]
    public void testOnPost()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Session = new Mock<ISession>().Object;
        var updateAccount = new Update_AccountModel
        {
            PageContext = new Microsoft.AspNetCore.Mvc.RazorPages.PageContext
            {
                HttpContext = httpContext
            },
            Email = "test@example.com",
            Username = "testuser",
            Password = "password123"
        };
    }
}