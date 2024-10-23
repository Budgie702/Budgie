namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;
    using Moq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Text;

    [TestClass]
    public class TestHomepage
    {
        [TestMethod]
        public void TestGetHomepage()
        {
            // Arrange
            var homepageModel = new HomepageModel();

            // Mock the HttpContext and Session
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            // Setup the session behavior
            byte[] usernameBytes = Encoding.UTF8.GetBytes("TestUser");
            mockSession.Setup(s => s.TryGetValue("Username", out usernameBytes)).Returns(true);

            // Assign the mocked session to the HttpContext
            mockHttpContext.Setup(c => c.Session).Returns(mockSession.Object);

            // Create a mock PageContext with the mocked HttpContext
            var pageContext = new PageContext
            {
                HttpContext = mockHttpContext.Object
            };

            homepageModel.PageContext = pageContext;

            // Act
            homepageModel.OnGet();

            // Assert
            Assert.IsNotNull(homepageModel.Username); // Ensure Username is set
            Assert.AreEqual("TestUser", homepageModel.Username); // Ensure it's the correct value
        }
    }
}
