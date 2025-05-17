using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_QA.PageObjectModel
{
    class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public HomePage(IWebDriver driver,WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public IWebElement acceptCookies=> wait.Until(d => d.FindElement(By.CssSelector("button[aria-label*='Accept the use of cookies']")));
        
        public void AcceptCookies()
        {
            acceptCookies.Click();
        }
    }
}
