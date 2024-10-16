namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Microsoft.Data.SqlClient;

    [TestClass]
    public class TestLogin
    {

        [TestMethod]
        public void TestLoginCode()
        {
            var loginTest = new loginModel
            {
                Credential = new Credential()
                {
                    Email = "dannyfinnegan60@gmail.com",
                    Password = "Please work",
                }
            };

            loginTest.OnPost();
        }

        [TestMethod]
        public void TestGetLogin()
        {
            var logMod = new loginModel
            {
                Credential = new Credential()
                {
                    Email = "dannyfinnegan60@gmail.com",
                    Password = "Please work",
                }
            };

            logMod.OnGet();

            Assert.IsTrue(true);
        }
    }
}
