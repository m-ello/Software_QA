using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectcentric.PageObjectModel
{
    class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public IWebElement usernameInputField => wait.Until(d => d.FindElement(By.Id("user-name")));
        public IWebElement passwordInputField => wait.Until(d => d.FindElement(By.Id("password")));
        public IWebElement loginButton => wait.Until(d => d.FindElement(By.Id("login-button")));

        public void Login(string username, string password)
        {
            usernameInputField.SendKeys(username);
            passwordInputField.SendKeys(password);
            loginButton.Click();
        }
    }
}
