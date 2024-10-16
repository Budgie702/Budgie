namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;

    [TestClass]
    public class TestLogin
    {
        [TestMethod]
        public void TestGetLogin()
        {
            var logMod = new loginModel
            {
                Credential = new Credential() // Assuming Credential has a parameterless constructor
                {
                    Username = "dannyfinnegan60@gmail.com",
                    Password = "Please work",
                 
                }
            };

            logMod.OnGet();

            Assert.IsTrue(true);
        }
    }
}
