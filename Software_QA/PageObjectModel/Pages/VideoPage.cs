using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Software_QA.PageObjectModel.Pages
{
    class VideoPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public VideoPage(IWebDriver driver,WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void ToggleFullscreen()
        {
            new Actions(driver).SendKeys("f").Perform();
            //driver.FindElement(By.CssSelector("button.ytp-fullscreen-button.ytp-button")).Click();
        }
        public IWebElement actionMenuButton => wait.Until(d =>
            d.FindElement(By.CssSelector("button[aria-label='Action menu']")));
        public IWebElement addToQueueButton => wait.Until(d => 
            d.FindElement(By.XPath("//div[@class='ytp-popup ytp-contextmenu']//div[contains(text(),'Add to queue')]")));
        public void NextVideo()
        {
            wait.Until(d =>
            {
                var element = d.FindElement(By.CssSelector("a.ytp-next-button.ytp-button"));
                return element.Displayed && element.Enabled ? element : null;
            }).Click();
        }
        public void AddToQueue()
        {
            wait.Until(d => actionMenuButton.Displayed && actionMenuButton.Enabled);
            actionMenuButton.Click();
            wait.Until(d => addToQueueButton.Displayed && addToQueueButton.Enabled);
            addToQueueButton.Click();
        }
    }
}
