namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Microsoft.Data.SqlClient;
    using BudgieBudgeting;

    [TestClass]
    public class TestLogin
    {

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

    [TestClass]
    public class TestLoginLogicPass
    {
        [TestMethod]
        public void TestLoginLogicMethod()
        {
            var logMod = new loginModel
            {
                Credential = new Credential()
                {
                    Email = "dannyfinnegan60@gmail.com",
                    Password = "Please work",
                }
            };

            logMod.OnPost();
            Assert.IsTrue(true);

        }
    }
    [TestClass]
    public class TestLoginLogicFailEmail
    {
        [TestMethod]
        public void TestLoginLogicMethod()
        {
            var logMod = new loginModel
            {
                Credential = new Credential()
                {
                    Email = "ThisShouldFail@gmail.com",
                    Password = "Please Dont Work",
                }
            };

            logMod.OnPost();
            Assert.AreEqual("Invalid email", logMod.ErrorMessage);

        }
    }
    [TestClass]
    public class TestLoginLogicFailPassword
    {
        [TestMethod]
        public void TestLoginLogicMethod()
        {
            var logMod = new loginModel
            {
                Credential = new Credential()
                {
                    Email = "dannyfinnegan60@gmail.com",
                    Password = "Please Dont Work",
                }
            };

            logMod.OnPost();
            Assert.AreEqual("Invalid password", logMod.ErrorMessage);

        }
    }
}
