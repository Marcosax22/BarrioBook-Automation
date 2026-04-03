using OpenQA.Selenium;
using BarrioBook.SeleniumTests.Helpers;

namespace BarrioBook.SeleniumTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelper _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelper(driver);
        }

        private By LoginEmail => By.Id("loginEmail");
        private By LoginPassword => By.Id("loginPassword");
        private By LoginSubmit => By.CssSelector("#loginForm button[type='submit']");
        private By LoginError => By.Id("loginError");
        private By AppView => By.Id("appView");
        private By UserInfo => By.Id("userInfo");

        public void Open(string baseUrl)
        {
            _driver.Navigate().GoToUrl(baseUrl);
        }

        public void Login(string email, string password)
        {
            _wait.UntilVisible(LoginEmail).Clear();
            _driver.FindElement(LoginEmail).SendKeys(email);

            _driver.FindElement(LoginPassword).Clear();
            _driver.FindElement(LoginPassword).SendKeys(password);

            _driver.FindElement(LoginSubmit).Click();
        }

        public bool IsLoggedIn()
        {
            return _wait.UntilVisible(AppView).Displayed;
        }

        public string GetUserInfo()
        {
            return _wait.UntilVisible(UserInfo).Text.Trim();
        }

        public string GetErrorMessage()
        {
            return _wait.UntilVisible(LoginError).Text.Trim();
        }
    }
}