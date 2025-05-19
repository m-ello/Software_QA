using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectcentric.PageObjectModel
{
    class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public IWebElement usernameInputField => wait.Until(d => d.FindElement(By.Id("user-name")));
        public IWebElement passwordInputField => wait.Until(d => d.FindElement(By.Id("password")));
        public IWebElement loginButton => wait.Until(d => d.FindElement(By.Id("login-button")));
        public IWebElement menuButton => wait.Until(d => d.FindElement(By.Id("react-burger-menu-btn")));
        public IWebElement logoutButton => wait.Until(d => d.FindElement(By.Id("logout_sidebar_link")));
        public IWebElement cartButton => wait.Until(d => d.FindElement(By.ClassName("shopping_cart_link")));
        public void SortLowToHigh()
        {
            var sortDropdown = driver.FindElement(By.ClassName("product_sort_container"));
            var selectElement = new SelectElement(sortDropdown);
            selectElement.SelectByValue("lohi");
        }

        public void Logout()
        {
            menuButton.Click();
            wait.Until(d => d.FindElement(By.Id("logout_sidebar_link")).Displayed);
            logoutButton.Click();
        }
        public IWebElement GetProductByName(string productName)
        {
            var product = driver.FindElement(By.XPath($"//div[text()='{productName}']/ancestor::div[@class='inventory_item']"));
            return product;
        }

        public void AddToCart(string productName)
        {
            var addToCartButton = GetProductByName(productName).FindElement(By.TagName("button"));
            addToCartButton.Click();
        }

        public void GoToCart()
        {
            cartButton.Click();
            wait.Until(d => d.Url.Contains("cart.html"));
        }
    }
}
