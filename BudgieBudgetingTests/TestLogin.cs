namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Microsoft.Data.SqlClient;
    using BudgieBudgeting;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc;

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
            // Mocking HttpContext and Session
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            // Setting up the session mock
            mockHttpContext.Setup(ctx => ctx.Session).Returns(mockSession.Object);

            // Creating the loginModel instance
            var logMod = new loginModel
            {
                Credential = new Credential()
                {
                    Email = "dannyfinnegan60@gmail.com",
                    Password = "Please work",
                }
            };

            // Assigning the mocked HttpContext to the PageModel's HttpContext
            logMod.PageContext.HttpContext = mockHttpContext.Object;

            // Mocking ModelState to be valid
            var modelState = new ModelStateDictionary();
            modelState.SetModelValue("Credential.Email", "dannyfinnegan60@gmail.com", "dannyfinnegan60@gmail.com");
            modelState.SetModelValue("Credential.Password", "Please work", "Please work");
            logMod.ModelState.Merge(modelState);

            // Ensure ModelState is valid
            logMod.ModelState.ClearValidationState("Credential");
            logMod.ModelState.MarkFieldValid("Credential.Email");
            logMod.ModelState.MarkFieldValid("Credential.Password");

            var result = logMod.OnPost();
            Assert.IsInstanceOfType(result, typeof(IActionResult));
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
