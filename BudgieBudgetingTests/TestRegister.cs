namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using Microsoft.Data.SqlClient;
    using Moq;

    [TestClass]
    public class TestRegister
    {
        [TestMethod]
        public void Register()
        {
            var mockRegisterModel = new Mock<RegisterModel>();
            mockRegisterModel.Setup(m => m.OnPost()).Verifiable();

            var registerTest = mockRegisterModel.Object;
            registerTest.RegisterCredential = new RegisterCredential()
            {
                Username = "Conor",
                Email = "conorDawson@gmail.com",
                Password = "Monster",
            };
            registerTest.ErrorMessage = string.Empty;

            registerTest.OnPost();

            mockRegisterModel.Verify(m => m.OnPost(), Times.Once);
        }
    }
}

