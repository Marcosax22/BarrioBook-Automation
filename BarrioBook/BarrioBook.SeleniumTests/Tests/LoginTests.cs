using NUnit.Framework;
using BarrioBook.SeleniumTests.Pages;

namespace BarrioBook.SeleniumTests.Tests
{
    public class LoginTests : BaseTest
    {
        [Test]
        public void Login_HappyPath_AdminCanLogin()
        {
            var loginPage = new LoginPage(Driver!);
            loginPage.Open(BaseUrl);

            loginPage.Login(AdminEmail, AdminPassword);

            Assert.That(loginPage.IsLoggedIn(), Is.True);
            Assert.That(loginPage.GetUserInfo(), Does.Contain("admin@barriobook.com").IgnoreCase);
        }

        [Test]
        public void Login_Negative_WrongPasswordShowsError()
        {
            var loginPage = new LoginPage(Driver!);
            loginPage.Open(BaseUrl);

            loginPage.Login(AdminEmail, "Incorrecta123");

            Assert.That(loginPage.GetUserInfo(), Does.Contain("No has iniciado sesión").IgnoreCase);
        }

        [Test]
        public void Login_Limit_EmptyFieldsStayOnLoginScreen()
        {
            var loginPage = new LoginPage(Driver!);
            loginPage.Open(BaseUrl);

            loginPage.Login("", "");

            Assert.That(Driver!.PageSource, Does.Contain("Iniciar sesión").IgnoreCase);
        }
    }
}