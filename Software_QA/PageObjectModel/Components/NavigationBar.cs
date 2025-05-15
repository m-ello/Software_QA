using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_QA.PageObjectModel.Components
{
    class NavigationBar
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public NavigationBar(IWebDriver driver,WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        
        public void SearchFor(string query)
        {
            IWebElement searchBar = wait.Until(d => d.FindElement(By.Name("search_query")));
            searchBar.SendKeys(query);
            IWebElement searchButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Search' and @title='Search']")));
            searchButton.Click();
            wait.Until(d => d.FindElements(By.CssSelector("ytd-video-renderer")).Count > 0);
        }
    }
}
