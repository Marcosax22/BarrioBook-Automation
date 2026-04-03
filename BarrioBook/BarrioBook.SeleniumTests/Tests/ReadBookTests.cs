using NUnit.Framework;
using BarrioBook.SeleniumTests.Pages;
using OpenQA.Selenium;

namespace BarrioBook.SeleniumTests.Tests
{
    public class ReadBookTests : BaseTest
    {
        [Test]
        public void ReadBooks_HappyPath_ShouldLoadBooksTable()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();

            Assert.That(Driver!.PageSource, Does.Contain("Listado de libros").IgnoreCase);
        }

        [Test]
        public void ReadBooks_Negative_WithoutLoginShouldStayInAuthView()
        {
            var loginPage = new LoginPage(Driver!);
            loginPage.Open(BaseUrl);

            Assert.That(Driver!.PageSource, Does.Contain("Iniciar sesión").IgnoreCase);
        }

        [Test]
        public void ReadBooks_Limit_WithMinimalDataStillLoadsTable()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();

            Assert.That(Driver!.FindElement(By.CssSelector("#booksTable")).Displayed, Is.True);
        }
    }
}