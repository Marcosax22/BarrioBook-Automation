using AventStack.ExtentReports;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using BarrioBook.SeleniumTests.Helpers;

namespace BarrioBook.SeleniumTests.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver? Driver;
        protected IConfiguration Config = null!;
        protected string BaseUrl = "";
        protected string AdminEmail = "";
        protected string AdminPassword = "";

        protected static ExtentReports Extent = null!;
        protected ExtentTest TestReport = null!;

        protected string ProjectRoot = "";
        protected string ScreenshotsFolder = "";
        protected string ReportsFolder = "";

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            BaseUrl = Config["BaseUrl"]!;
            AdminEmail = Config["AdminEmail"]!;
            AdminPassword = Config["AdminPassword"]!;

            ProjectRoot = GetProjectRoot();
            ScreenshotsFolder = Path.Combine(ProjectRoot, "Screenshots");
            ReportsFolder = Path.Combine(ProjectRoot, "Reports");

            Directory.CreateDirectory(ScreenshotsFolder);
            Directory.CreateDirectory(ReportsFolder);

            var reportPath = Path.Combine(ReportsFolder, "BarrioBookSeleniumReport.html");

            Extent = ReportManager.GetInstance(reportPath);
        }

        [SetUp]
        public void Setup()
        {
            Driver = DriverFactory.CreateChromeDriver();
            TestReport = Extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (Driver != null)
                {
                    var screenshotPath = ScreenshotHelper.Take(
                        Driver,
                        ScreenshotsFolder,
                        TestContext.CurrentContext.Test.Name
                    );

                    if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                    {
                        TestReport.Fail($"Test falló. Screenshot: {screenshotPath}");
                        TestReport.AddScreenCaptureFromPath(screenshotPath);
                    }
                    else
                    {
                        TestReport.Pass($"Test pasó. Screenshot: {screenshotPath}");
                        TestReport.AddScreenCaptureFromPath(screenshotPath);
                    }
                }
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver.Dispose();
                    Driver = null;
                }

                Extent.Flush();
            }
        }

        private string GetProjectRoot()
        {
            var dir = TestContext.CurrentContext.TestDirectory;

            while (dir != null)
            {
                var candidate = Path.Combine(dir, "BarrioBook.SeleniumTests.csproj");
                if (File.Exists(candidate))
                    return dir;

                dir = Directory.GetParent(dir)?.FullName;
            }

            throw new DirectoryNotFoundException("No se pudo encontrar la raíz del proyecto BarrioBook.SeleniumTests.");
        }
    }
}