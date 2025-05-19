using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectcentric.PageObjectModel
{
    class YourCartPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public YourCartPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        IWebElement checkoutButton => wait.Until(d => d.FindElement(By.Id("checkout")));
        

        public void Checkout()
        {
            checkoutButton.Click();
            wait.Until(d => d.Url.Contains("checkout-step-one"));
            driver.FindElement(By.Id("first-name")).SendKeys("Elon");
            driver.FindElement(By.Id("last-name")).SendKeys("Musk");
            driver.FindElement(By.Id("postal-code")).SendKeys("111118");
            driver.FindElement(By.Id("continue")).Click();

            wait.Until(d => d.Url.Contains("checkout-step-two"));
            driver.FindElement(By.Id("finish")).Click();
            wait.Until(d => d.Url.Contains("checkout-complete"));
        }
    }
}
