namespace BookStore.Web.Models
{
    public class BookDto
    {
        public int BookId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public double UnitPrice { get; set; }

        public int Stock { get; set; }

        public string ISBNNO { get; set; }

        public string Publisher { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public int Count { get; set; } = 1;
    }
}
