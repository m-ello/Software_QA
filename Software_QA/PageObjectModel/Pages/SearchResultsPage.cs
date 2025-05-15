using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_QA.PageObjectModel.Pages
{
    class SearchResultsPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public SearchResultsPage(IWebDriver driver,WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void OpenVideoByTitle(string partialTitle)
        {
            string xpath = $"//ytd-video-renderer//a[@id='video-title'][contains(translate(@title, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{partialTitle.ToLower()}')]";

            IWebElement video = wait.Until(d => d.FindElement(By.XPath(xpath)));
            video.Click();
        }
    }
}
