using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace BarrioBook.SeleniumTests.Helpers
{
    public static class ReportManager
    {
        private static ExtentReports? _extent;
        private static readonly object LockObj = new();

        public static ExtentReports GetInstance(string reportPath)
        {
            if (_extent == null)
            {
                lock (LockObj)
                {
                    if (_extent == null)
                    {
                        var spark = new ExtentSparkReporter(reportPath);
                        spark.Config.DocumentTitle = "BarrioBook Selenium Report";
                        spark.Config.ReportName = "Resultados de pruebas automatizadas";

                        _extent = new ExtentReports();
                        _extent.AttachReporter(spark);
                    }
                }
            }

            return _extent;
        }
    }
}