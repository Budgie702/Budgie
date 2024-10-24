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
    public class TestUpdateAccountSelenium
    {
        private IWebDriver driver;
        private Webdriver WebDriver;


        [TestMethod]
        public void TestRegister()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Update_Account");
            WebDriver.retryableSeleniumElementSend("email", "dannyfinnegan60@gmail.com");
            WebDriver.retryableSeleniumElementSend("user", "Danny");
            WebDriver.retryableSeleniumElementSend("password", "Please work");
            WebDriver.retryableSeleniumElementClick("update");
            WebDriver.quit();
        }
        [TestMethod]
        public void TestRegisterFailEmail()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Update_Account");
            WebDriver.retryableSeleniumElementSend("email", "");
            WebDriver.retryableSeleniumElementSend("user", "Danny");
            WebDriver.retryableSeleniumElementSend("password", "Please work");
            WebDriver.retryableSeleniumElementClick("update");
            WebDriver.retryableSeleniumFailElement_Check("email-error");
            WebDriver.quit();
        }

        [TestMethod]
        public void TestRegisterFailPassword()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Update_Account");
            WebDriver.retryableSeleniumElementSend("email", "dannyfinnegan60@gmail.com");
            WebDriver.retryableSeleniumElementSend("user", "Danny");
            WebDriver.retryableSeleniumElementSend("password", "");
            WebDriver.retryableSeleniumElementClick("update");
            WebDriver.retryableSeleniumFailElement_Check("password-error");
            WebDriver.quit();
        }
        [TestMethod]
        public void TestRegisterFailUser()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Update_Account");
            WebDriver.retryableSeleniumElementSend("email", "dannyfinnegan60@gmail.com");
            WebDriver.retryableSeleniumElementSend("user", "");
            WebDriver.retryableSeleniumElementSend("password", "Please work");
            WebDriver.retryableSeleniumElementClick("update");
            WebDriver.retryableSeleniumFailElement_Check("user-error");
            WebDriver.quit();
        }

        [TestMethod]
        public void TestRegisterOnGet()
        {
            WebDriver = new Webdriver();
            WebDriver.retryableSeleniumURL("http://localhost:5185/Update_Account");
            WebDriver.quit();
        }
    }
}

