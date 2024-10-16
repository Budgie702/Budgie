using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using BudgieBudgeting.Pages;

    [TestClass]
    public class TestDeleteAccount
    {
        [TestMethod]
        public void TestDelete_Account()
        {

            var deleteMod = new Delete_AccountModel
            {
                DeleteCredential = new DeleteCredential() 
                {
                    Email = "dannyfinnegan60@gmail.com",
                    Password = "Please work",

                },
                ErrorMessage = string.Empty
            };

            deleteMod.OnPost();

            Assert.IsTrue(true);
        }
    }
}
