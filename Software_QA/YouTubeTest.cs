using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Software_QA
{
    [TestClass]
    public class YouTubeTest
    {
        [TestMethod]
        public void PlayVideo1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.youtube.com");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div[6]/div[1]/ytd-button-renderer[2]/yt-button-shape/button")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Name("search_query")).SendKeys("behind blue eyes");
            driver.FindElement(By.XPath("//button[@aria-label='Search' and @title='Search']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//a[@id='video-title' and @title=concat('Limp Bizkit ', '\"', 'Behind Blue Eyes', '\"')]")).Click();
        }
    }
}
