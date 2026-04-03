using NUnit.Framework;
using BarrioBook.SeleniumTests.Pages;

namespace BarrioBook.SeleniumTests.Tests
{
    public class UpdateBookTests : BaseTest
    {
        [Test]
        public void UpdateBook_HappyPath_ShouldUpdateBookTitle()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            var originalTitle = $"Libro Editar {Guid.NewGuid().ToString("N")[..8]}";
            var updatedTitle = $"{originalTitle} Editado";

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(originalTitle, "Autor Base", "100", "3");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(originalTitle), Is.True);

            booksPage.ClickEditByTitle(originalTitle);
            booksPage.FillBookForm(updatedTitle, "Autor Base", "150", "8");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(updatedTitle), Is.True);
        }

        [Test]
        public void UpdateBook_Negative_InvalidDataShouldNotPersist()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            var originalTitle = $"Libro Invalidar {Guid.NewGuid().ToString("N")[..8]}";

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(originalTitle, "Autor Base", "100", "3");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(originalTitle), Is.True);

            booksPage.ClickEditByTitle(originalTitle);
            booksPage.FillBookForm("", "Autor Base", "100", "3");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(originalTitle), Is.True);
        }

        [Test]
        public void UpdateBook_Limit_StockZeroShouldPersist()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            var originalTitle = $"Libro StockUpdate {Guid.NewGuid().ToString("N")[..8]}";

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(originalTitle, "Autor Base", "100", "3");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(originalTitle), Is.True);

            booksPage.ClickEditByTitle(originalTitle);
            booksPage.FillBookForm(originalTitle, "Autor Base", "100", "0");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(originalTitle), Is.True);
        }
    }
}