namespace BudgieBudgetingTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgieBudgeting.Pages.Shared;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetHomepage()
        {
            var homepageModel = new HomepageModel();

            homepageModel.OnGet();

            Assert.IsTrue(true);
        }
    }
}
