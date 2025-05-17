using System;
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

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            loginPage = new LoginPage(driver, wait);
            homePage = new HomePage(driver, wait);

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
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
            Login();
            homePage.Logout();

            wait.Until(d => d.Url.Contains("saucedemo.com"));
            Assert.IsTrue(driver.Url.Contains("login"));
        }

        [TestMethod]
        public void AddToCart()
        {
            Login();

            string productName = "Sauce Labs Backpack";

            homePage.AddToCart(productName);
            homePage.cartButton.Click();

            Assert.IsTrue(driver.PageSource.Contains(productName));
        }

        [TestMethod]
        public void Checkout()
        {
            AddToCart();
            driver.FindElement(By.Id("checkout")).Click();
            wait.Until(d => d.Url.Contains("checkout-step-one"));

            // ✍️ Completează formularul de checkout
            driver.FindElement(By.Id("first-name")).SendKeys("Ion");
            driver.FindElement(By.Id("last-name")).SendKeys("Popescu");
            driver.FindElement(By.Id("postal-code")).SendKeys("123456");
            driver.FindElement(By.Id("continue")).Click();

            wait.Until(d => d.Url.Contains("checkout-step-two"));
            driver.FindElement(By.Id("finish")).Click();

            // ✅ Verificare mesaj final
            wait.Until(d => d.Url.Contains("checkout-complete"));
            bool isSuccess = driver.PageSource.Contains("THANK YOU FOR YOUR ORDER");

            Console.WriteLine(isSuccess
                ? "❌ Comanda NU s-a finalizat corect!"
                : "🎉 Comandă finalizată cu succes!");
        }

        [TestMethod]
        public void maimulte()
        {
            var options = new ChromeOptions();
            options.AddArguments("incognito");
            IWebDriver driver = new ChromeDriver(options);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // 1. Logare
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            // Aștept să apară pagina de produse
            wait.Until(d => d.FindElement(By.ClassName("inventory_list")));

            // 2. Sortează elementele după "Price (low to high)"
            var sortDropdown = driver.FindElement(By.ClassName("product_sort_container"));
            var selectElement = new SelectElement(sortDropdown);
            selectElement.SelectByValue("lohi");

            // 3. Adaugă 3 elemente în coș
            // Pentru exemplu, adaug primele 3 produse vizibile (prin butoane Add to cart)
            var addButtons = driver.FindElements(By.CssSelector("button.btn_inventory"));
            for (int i = 0; i < 3; i++)
            {
                addButtons[i].Click();
            }

            // 4. Verifică dacă sunt 3 în coș
            var cartBadge = driver.FindElement(By.ClassName("shopping_cart_badge"));
            if (cartBadge.Text != "3")
            {
                Console.WriteLine("EROARE: Numărul de produse din coș NU este 3, este " + cartBadge.Text);
            }
            else
            {
                Console.WriteLine("Succes: Sunt 3 produse în coș.");
            }

            // 5. Mergi în coș
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();

            // Așteaptă lista de produse din coș
            wait.Until(d => d.FindElements(By.ClassName("cart_item")).Count > 0);

            // 6. Șterge 2 produse (primele 2 butoane Remove)
            var removeButtons = driver.FindElements(By.CssSelector("button.cart_button"));
            removeButtons[0].Click();
            removeButtons[1].Click();

            // 7. Verifică dacă au rămas 1 produs în coș
            var remainingItems = driver.FindElements(By.ClassName("cart_item")).Count;
            if (remainingItems != 1)
            {
                Console.WriteLine("EROARE: Numărul de produse rămas în coș NU este 1, este " + remainingItems);
            }
            else
            {
                Console.WriteLine("Succes: Au rămas 1 produs în coș după ștergere.");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
       
    }
}
