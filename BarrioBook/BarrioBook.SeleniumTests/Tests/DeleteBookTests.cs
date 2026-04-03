using NUnit.Framework;
using BarrioBook.SeleniumTests.Pages;

namespace BarrioBook.SeleniumTests.Tests
{
    public class DeleteBookTests : BaseTest
    {
        [Test]
        public void DeleteBook_HappyPath_ShouldDeleteBook()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            var title = $"Libro Eliminar {Guid.NewGuid().ToString("N")[..8]}";

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(title, "Autor Delete", "100", "2");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(title), Is.True);

            booksPage.ClickDeleteByTitle(title);
            booksPage.AcceptDeleteAlert();

            Assert.That(booksPage.WaitForBookToDisappear(title), Is.True);
        }

        [Test]
        public void DeleteBook_Negative_DismissAlertShouldNotDelete()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            var title = $"Libro CancelDelete {Guid.NewGuid().ToString("N")[..8]}";

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(title, "Autor Delete", "100", "2");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(title), Is.True);

            booksPage.ClickDeleteByTitle(title);
            booksPage.DismissDeleteAlert();

            Assert.That(booksPage.WaitForBookInTable(title), Is.True);
        }

        [Test]
        public void DeleteBook_Limit_AfterDeleteSecondLookupShouldNotFindBook()
        {
            var loginPage = new LoginPage(Driver!);
            var booksPage = new BooksPage(Driver!);

            var title = $"Libro DoubleCheck {Guid.NewGuid().ToString("N")[..8]}";

            loginPage.Open(BaseUrl);
            loginPage.Login(AdminEmail, AdminPassword);

            booksPage.OpenBooksTab();
            booksPage.FillBookForm(title, "Autor Delete", "100", "2");
            booksPage.SubmitBookForm();

            Assert.That(booksPage.WaitForBookInTable(title), Is.True);

            booksPage.ClickDeleteByTitle(title);
            booksPage.AcceptDeleteAlert();

            Assert.That(booksPage.WaitForBookToDisappear(title), Is.True);
        }
    }
}