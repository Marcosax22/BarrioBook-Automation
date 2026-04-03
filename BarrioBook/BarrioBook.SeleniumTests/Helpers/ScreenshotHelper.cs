using OpenQA.Selenium;

namespace BarrioBook.SeleniumTests.Helpers
{
    public static class ScreenshotHelper
    {
        public static string Take(IWebDriver driver, string folder, string namePrefix)
        {
            Directory.CreateDirectory(folder);

            var safeName = string.Join("_", namePrefix.Split(Path.GetInvalidFileNameChars()));
            var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{safeName}.png";
            var fullPath = Path.Combine(folder, fileName);

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fullPath);

            return fullPath;
        }
    }
}