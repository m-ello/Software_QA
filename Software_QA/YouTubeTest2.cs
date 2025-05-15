using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Software_QA
{
    [TestClass]
    public class YouTubeTest2
    {
        [TestMethod]
        public void AddtoQueue()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.youtube.com/watch?v=5dlubcRwYnI");
            driver.Manage().Window.Maximize();
            Thread.Sleep(3000);
            //driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div[6]/div[1]/ytd-button-renderer[2]/yt-button-shape/button")).Click();
            var acceptButton = driver.FindElement(By.CssSelector("button[aria-label*='Accept the use of cookies']"));
            acceptButton.Click();
            Thread.Sleep(3000);
            var actionMenuButton = driver.FindElement(By.CssSelector("button[aria-label='Action menu']"));
            actionMenuButton.Click();
            var addToQueueOption = driver.FindElement(By.XPath("//yt-formatted-string[text()='Add to queue']"));
            addToQueueOption.Click();
            
            Thread.Sleep(20000);

            /*
            driver.FindElement(By.XPath("//button[.//span[text()='Create account']]")).Click();
            driver.FindElement(By.XPath("//li[.//span[text()='For my personal use']]")).Click();

            driver.FindElement(By.Id("firstName")).SendKeys("admin");
            driver.FindElement(By.Id("lastName")).SendKeys("admin");
            driver.FindElement(By.XPath("//button[.//span[text()='Next']]")).Click();

            Thread.Sleep(10000);

            driver.FindElement(By.Id("day")).SendKeys("15");

            driver.FindElement(By.Id("month")).Click();
            driver.FindElement(By.XPath("//li[.//span[text()='January']]")).Click();     

            driver.FindElement(By.Id("year")).SendKeys("2000");

            driver.FindElement(By.Id("gender")).Click();
            driver.FindElement(By.XPath("//li[.//span[text()='Male']]")).Click();


            driver.FindElement(By.XPath("//button[.//span[text()='Next']]")).Click();

            Thread.Sleep(10000);*/
            driver.Quit();
        }
    }
}