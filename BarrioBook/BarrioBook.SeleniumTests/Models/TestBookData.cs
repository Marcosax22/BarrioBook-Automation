namespace BarrioBook.SeleniumTests.Models
{
    public class TestBookData
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Price { get; set; } = "";
        public string Stock { get; set; } = "";
        public string SupplierId { get; set; } = "";
        public string ImageUrl { get; set; } = "";

        public static TestBookData Valid(string suffix)
        {
            return new TestBookData
            {
                Title = $"Libro Selenium {suffix}",
                Author = "Marcos Encarnación",
                Price = "599",
                Stock = "12",
                SupplierId = "",
                ImageUrl = ""
            };
        }
    }
}