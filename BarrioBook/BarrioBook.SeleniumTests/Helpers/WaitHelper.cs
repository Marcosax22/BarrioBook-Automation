using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BarrioBook.SeleniumTests.Helpers
{
    public class WaitHelper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public WaitHelper(IWebDriver driver, int seconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }

        public IWebElement UntilVisible(By by)
        {
            return _wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(by);
                    return element.Displayed ? element : null;
                }
                catch
                {
                    return null;
                }
            })!;
        }

        public IWebElement UntilClickable(By by)
        {
            return _wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(by);
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch
                {
                    return null;
                }
            })!;
        }
    }
}