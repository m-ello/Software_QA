using System;
using System.Security.Permissions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using proiectcentric.PageObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace proiectcentric
{
    [TestClass]
    public class SauceDemoTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private LoginPage loginPage;
        private HomePage homePage;
        private YourCartPage cartPage;

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArguments("incognito");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            loginPage = new LoginPage(driver, wait);
            homePage = new HomePage(driver, wait);
            cartPage = new YourCartPage(driver, wait);

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Window.Maximize();
            Login();
        }

        public void Login()
        {
            string username = "standard_user";
            string password = "secret_sauce";

            loginPage.Login(username, password);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("inventory.html"));

            Assert.IsTrue(driver.Url.Contains("inventory.html"));
        }

        [TestMethod]
        public void Logout() 
        {
            homePage.Logout();

            wait.Until(d => d.Url.Contains("saucedemo.com"));
            Assert.IsTrue(driver.PageSource.Contains("Login"));
        }

        [TestMethod]
        public void AddToCart()
        {
            string productName = "Sauce Labs Backpack";

            homePage.AddToCart(productName);
            homePage.cartButton.Click();

            Assert.IsTrue(driver.PageSource.Contains(productName));
        }

        [TestMethod]
        public void Checkout()
        {
            AddToCart();
            homePage.GoToCart();
            cartPage.Checkout();
            Assert.IsTrue(driver.PageSource.Contains("Thank you for your order"));
        }

        [TestMethod]
        public void CheckCartBadge()
        {
            int itemsToAdd = 3;
            wait.Until(d => d.FindElement(By.ClassName("inventory_list")));
            AddNItemsToCart(itemsToAdd);

            var cartBadge = driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.IsTrue(cartBadge.Text == itemsToAdd.ToString());
        }
        public void AddNItemsToCart(int itemsToAdd = 1)
        {
            wait.Until(d => d.FindElement(By.ClassName("inventory_list")));

            var addButtons = driver.FindElements(By.CssSelector("button.btn_inventory"));
            for (int i = 0; i < itemsToAdd; i++)
            {
                addButtons[i].Click();
            }

            var cartBadge = driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.IsTrue(cartBadge.Text == itemsToAdd.ToString());
        }

        [TestMethod]
        public void RemoveFromCart()
        {
            var itemsToAdd = 4;
            AddNItemsToCart(itemsToAdd);
            homePage.GoToCart();
            wait.Until(d => d.FindElements(By.ClassName("cart_item")).Count > 0);

            var itemsToRemove = 2;
            var removeButtons = driver.FindElements(By.CssSelector("button.cart_button"));
            for (int i = 0; i < itemsToRemove && i < removeButtons.Count; i++)
            {
                removeButtons[removeButtons.Count - 1 - i].Click();
            }

            var remainingItems = driver.FindElements(By.ClassName("cart_item")).Count;
            var expectedValue = itemsToAdd - itemsToRemove;

            Assert.IsTrue(remainingItems == expectedValue);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
       
    }
}
