using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BarrioBook.SeleniumTests.Helpers
{
    public static class DriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-insecure-localhost");

            return new ChromeDriver(options);
        }
    }
}