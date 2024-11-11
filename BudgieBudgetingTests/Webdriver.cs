using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgieBudgetingTests
{
    public class Webdriver
    {
        IWebDriver driver;
        public Webdriver()
        {
            try
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--window-size=1920,1080");
                driver = new ChromeDriver(chromeOptions);
            }
            catch (Exception e)
            {
                throw new Exception("Driver not found");
            }
        }

        public void quit()
        {
            Thread.Sleep(1000);
            driver.Quit();
        }

        public void retryableSeleniumURL(string url)
        {
            int retries = 0;
            while (retries < 3)
            {
                try
                {
                    driver.Navigate().GoToUrl(url);
                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(1000);
                    retries++;
                }
            }
        }
       public void retryableSeleniumElementSend(string id, string text)
        {
            int retries = 0;
            while (retries < 3)
            {
                try
                {
                    driver.FindElement(By.Id(id)).SendKeys(text);
                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(1000);
                    retries++;
                }
            }
        }

        public void retryableSeleniumElementClick(string id)
        {
            int retries = 0;
            while (retries < 3)
            {
                try
                {
                    driver.FindElement(By.Id(id)).Click();
                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(1000);
                    retries++;
                }
            }
        }

        public void retryableSeleniumFailElement_Check (string id)
        {
            int retries = 0;
            while (retries < 3)
            {
                try
                {
                    driver.FindElement(By.Id(id)).Click();
                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(1000);
                    retries++;
                }
            }
        }

    }
}
