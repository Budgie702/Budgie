using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgieBudgetingTests
{
    [TestClass]
    public class TestLoginSelenium
    {
        private IWebDriver driver;
        private Webdriver WebDriver;

        public TestLoginSelenium()
        {
            Webdriver driver = new Webdriver();
        }

        [TestMethod]
        public void TestLogin()
        {
            WebDriver.retryableSeleniumURL("http://localhost:5185/Login");
            WebDriver.retryableSeleniumElementSend("email", "dannyfinnegan60@gmail.com");
            WebDriver.retryableSeleniumElementSend("password", "Please work");
            WebDriver.retryableSeleniumElementClick("submit");
        }
    }
}
