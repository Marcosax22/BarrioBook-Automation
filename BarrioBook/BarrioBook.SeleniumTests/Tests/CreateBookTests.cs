using NUnit.Framework;
using BarrioBook.SeleniumTests.Models;
using BarrioBook.SeleniumTests.Pages;

namespace BarrioBook.SeleniumTests.Tests
{
    public class CreateBookTests : BaseTest
    {
        [Test]
        public void CreateBook_HappyPath_ShouldCreateBook()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);
            var book = TestBookData.Valid(Guid.NewGuid().ToString("N")[..8]);

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(book.Title, book.Author, book.Price, book.Stock, book.SupplierId, book.ImageUrl);
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(book.Title), Is.True);
        }

        [Test]
        public void CreateBook_Negative_MissingTitleShouldFail()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm("", "Autor sin título", "100", "5");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.TableContainsText("Autor sin título"), Is.False);
        }

        [Test]
        public void CreateBook_Limit_ZeroStockShouldBeAccepted()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);
            var title = $"Libro Stock 0 {Guid.NewGuid().ToString("N")[..8]}";

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(title, "Autor Límite", "50", "0");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(title), Is.True);
        }
    }
}
