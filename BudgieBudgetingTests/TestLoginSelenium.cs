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


        [TestMethod]
        public void TestLogin()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Login");
            WebDriver.retryableSeleniumElementSend("email", "dannyfinnegan60@gmail.com");
            WebDriver.retryableSeleniumElementSend("password", "Please work");
            WebDriver.retryableSeleniumElementClick("login");
            WebDriver.quit();
        }

        [TestMethod]
        public void TestLoginFailEmail()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Login");
            WebDriver.retryableSeleniumElementSend("email", "");
            WebDriver.retryableSeleniumElementSend("password", "Please work");
            WebDriver.retryableSeleniumElementClick("login");
            WebDriver.retryableSeleniumFailElement_Check("email-error");
            WebDriver.quit();
        }
        [TestMethod]
        public void TestLoginFailPassword()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Login");
            WebDriver.retryableSeleniumElementSend("email", "dannyfinnegan60@gmail.com");
            WebDriver.retryableSeleniumElementSend("password", "");
            WebDriver.retryableSeleniumElementClick("login");
            WebDriver.retryableSeleniumFailElement_Check("password-error");
            WebDriver.quit();
        }
        [TestMethod]
        public void TestLoginGet()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Login");
            WebDriver.quit();
        }
    }
}

