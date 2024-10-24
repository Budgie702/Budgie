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
            driver = new ChromeDriver("chromedriver.exe");
        }

        public void retryableSeleniumURL(string url)
        {
            int retries = 0;
            while (retries < 3)
            {
                try
                {
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(1000);
                    retries++;
                }
                catch (Exception e)
                {

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
                    Thread.Sleep(1000);
                    retries++;
                }
                catch (Exception e)
                {
                    if (retries == 3)
                    {
                        throw new Exception("Element not found" + id);
                    }
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
                    Thread.Sleep(1000);
                    retries++;
                }
                catch (Exception e)
                {
                    if (retries == 3)
                    {
                        throw new Exception("Element not found" + id);
                    }
                }
            }
        }
    }
}
