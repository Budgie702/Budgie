namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using Microsoft.Data.SqlClient;
    using Moq;
    using BudgieBudgeting.Pages;
    using Microsoft.AspNetCore.Http;

    [TestClass]
    public class TestUpdate_Spending
    {
        [TestMethod]
        public void TestOnGet()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new Mock<ISession>().Object;
            var updateSpending = new Update_SpendingModel
            {
                PageContext = new Microsoft.AspNetCore.Mvc.RazorPages.PageContext
                {
                    HttpContext = httpContext
                }
            };
        }
    }
}


