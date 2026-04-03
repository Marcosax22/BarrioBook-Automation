using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BarrioBook.SeleniumTests.Helpers;

namespace BarrioBook.SeleniumTests.Pages
{
    public class BooksPage
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelper _wait;

        public BooksPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelper(driver, 20);
        }

        private By BooksTab => By.CssSelector(".tab[data-view='booksView']");
        private By RefreshBooksBtn => By.Id("refreshBooksBtn");
        private By BooksTableBody => By.CssSelector("#booksTable tbody");

        private By BookTitle => By.Id("bookTitle");
        private By BookAuthor => By.Id("bookAuthor");
        private By BookPrice => By.Id("bookPrice");
        private By BookStock => By.Id("bookStock");
        private By BookSupplierId => By.Id("bookSupplierId");
        private By BookImageUrl => By.Id("bookImageUrl");
        private By BookForm => By.Id("bookForm");
        private By BookFormTitle => By.Id("bookFormTitle");
        private By BookFormError => By.Id("bookFormError");
        private By BookReset => By.Id("bookFormReset");

        public void OpenBooksTab()
        {
            _wait.UntilClickable(BooksTab).Click();
            RefreshBooks();
        }

        public void RefreshBooks()
        {
            _wait.UntilClickable(RefreshBooksBtn).Click();
            _wait.UntilVisible(BooksTableBody);
        }

        public void FillBookForm(string title, string author, string price, string stock, string supplierId = "", string imageUrl = "")
        {
            _driver.FindElement(BookTitle).Clear();
            _driver.FindElement(BookTitle).SendKeys(title);

            _driver.FindElement(BookAuthor).Clear();
            _driver.FindElement(BookAuthor).SendKeys(author);

            _driver.FindElement(BookPrice).Clear();
            _driver.FindElement(BookPrice).SendKeys(price);

            _driver.FindElement(BookStock).Clear();
            _driver.FindElement(BookStock).SendKeys(stock);

            _driver.FindElement(BookSupplierId).Clear();
            if (!string.IsNullOrWhiteSpace(supplierId))
                _driver.FindElement(BookSupplierId).SendKeys(supplierId);

            _driver.FindElement(BookImageUrl).Clear();
            if (!string.IsNullOrWhiteSpace(imageUrl))
                _driver.FindElement(BookImageUrl).SendKeys(imageUrl);
        }

        public void SubmitBookForm()
        {
            _driver.FindElement(BookForm).Submit();
        }

        public void ResetForm()
        {
            _driver.FindElement(BookReset).Click();
        }

        public string GetFormTitle()
        {
            return _driver.FindElement(BookFormTitle).Text.Trim();
        }

        public string GetFormError()
        {
            return _driver.FindElement(BookFormError).Text.Trim();
        }

        public bool TableContainsText(string text)
        {
            var tableText = _wait.UntilVisible(BooksTableBody).Text;
            return tableText.Contains(text, StringComparison.OrdinalIgnoreCase);
        }

        public bool WaitForBookInTable(string title, int seconds = 20)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

            return wait.Until(driver =>
            {
                try
                {
                    RefreshBooks();
                    var bodyText = driver.FindElement(BooksTableBody).Text;
                    return bodyText.Contains(title, StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
            });
        }

        public bool WaitForBookToDisappear(string title, int seconds = 20)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

            return wait.Until(driver =>
            {
                try
                {
                    RefreshBooks();
                    var bodyText = driver.FindElement(BooksTableBody).Text;
                    return !bodyText.Contains(title, StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return true;
                }
            });
        }

        public void ClickEditByTitle(string title)
        {
            WaitForBookInTable(title);

            var xpath = $"//table[@id='booksTable']//tr[td[contains(normalize-space(),'{title}')]]//button[contains(.,'Editar')]";
            var button = new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(driver => driver.FindElement(By.XPath(xpath)));

            button.Click();
        }

        public void ClickDeleteByTitle(string title)
        {
            WaitForBookInTable(title);

            var xpath = $"//table[@id='booksTable']//tr[td[contains(normalize-space(),'{title}')]]//button[contains(.,'Borrar')]";
            var button = new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(driver => driver.FindElement(By.XPath(xpath)));

            button.Click();
        }

        public void AcceptDeleteAlert()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
                .Until(driver =>
                {
                    try
                    {
                        driver.SwitchTo().Alert().Accept();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
        }

        public void DismissDeleteAlert()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
                .Until(driver =>
                {
                    try
                    {
                        driver.SwitchTo().Alert().Dismiss();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
        }
    }
}