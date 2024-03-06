using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BookStore.Services.BookAPI.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public double UnitPrice { get; set; }

        public int Stock { get; set; }

        public string ISBNNO { get; set; }

        public string Publisher { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }
    }
}
