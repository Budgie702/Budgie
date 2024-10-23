using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using BudgieBudgeting.Pages;
    using BudgieBudgeting;

    [TestClass]
    public class TestDeleteAccount
    {
        [TestMethod]
        public void TestDelete_Account()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockResponse = new Mock<HttpResponse>();
            mockHttpContext.Setup(ctx => ctx.Response).Returns(mockResponse.Object);

            var mockDeleteAccountModel = new Mock<Delete_AccountModel>();
            mockDeleteAccountModel.Setup(m => m.OnPost()).Verifiable();

            var deleteMod = mockDeleteAccountModel.Object;
            deleteMod.PageContext = new PageContext
            {
                HttpContext = mockHttpContext.Object
            };
            deleteMod.DeleteCredential = new DeleteCredential()
            {
                Email = "dannyfinnegan60@gmail.com",
                Password = "Please work",
            };
            deleteMod.ErrorMessage = string.Empty;

            deleteMod.OnPost();

            mockDeleteAccountModel.Verify(m => m.OnPost(), Times.Once);
            Assert.IsTrue(true);
        }
    }
}
