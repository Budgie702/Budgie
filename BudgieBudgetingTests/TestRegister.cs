namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using Microsoft.Data.SqlClient;

    [TestClass]
    public class TestRegister
    {
        [TestMethod]
        public void Register()
        {
            var registerTest = new RegisterModel
            {
                RegisterCredential = new RegisterCredential()
                {
                    Username = "Conor",
                    Email = "conorDawson@gmail.com",
                    Password = "Monster",
                },
                ErrorMessage = string.Empty
            };

            var checkCommand = new SqlCommand();
            checkCommand.Parameters.AddWithValue("@Email", registerTest.RegisterCredential.Email);

            registerTest.OnPost();
        }
    }
}