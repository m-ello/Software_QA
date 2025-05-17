using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Software_QA.PageObjectModel;
using Software_QA.PageObjectModel.Components;
using Software_QA.PageObjectModel.Pages;
using static System.Net.Mime.MediaTypeNames;

namespace Software_QA
{
    [TestClass]
    public class YouTubeTest
    {
        private IWebDriver driver;
        private HomePage homePage;
        private VideoPage videoPage;
        private NavigationBar navigationBar;
        private SearchResultsPage searchResultsPage;
        private WebDriverWait wait;

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArguments("incognito");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.youtube.com");

            homePage = new HomePage(driver, wait);
            homePage.AcceptCookies();
        }

        [TestMethod]
        public void PlayVideo()
        {
            videoPage = new VideoPage(driver, wait);
            navigationBar = new NavigationBar(driver, wait);
            searchResultsPage = new SearchResultsPage(driver, wait);

            string query = "no one like you";
            navigationBar.SearchFor(query);
            
            wait.Until(d => d.FindElement(By.CssSelector("ytd-video-renderer")));
            searchResultsPage.OpenVideoByTitle("no one like you");


            wait.Until(d => d.FindElement(By.CssSelector(".html5-video-player")));
            videoPage.ToggleFullscreen();
            
            
            Thread.Sleep(5000);
            videoPage.ToggleFullscreen();
        }

        [TestMethod]
        public void AddVideoToQueue()
        {
            videoPage = new VideoPage(driver, wait);
            navigationBar = new NavigationBar(driver, wait);
            searchResultsPage = new SearchResultsPage(driver,wait);

            string query = "10 seconds timer";
            navigationBar.SearchFor(query);

            searchResultsPage.OpenVideoByTitle("10 seconds timer");
            wait.Until(d => d.FindElement(By.CssSelector(".html5-video-player")));
            videoPage.AddToQueue();
            videoPage.NextVideo();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
