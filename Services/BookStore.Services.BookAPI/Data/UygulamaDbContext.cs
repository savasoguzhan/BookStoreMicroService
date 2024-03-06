using BookStore.Services.BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.BookAPI.Data
{
    public class UygulamaDbContext : DbContext
    {

        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
    new Book
    {
        BookId = 1,
        Name = "The Great Gatsby",
        CategoryName = "Fiction",
        UnitPrice = 12.99,
        Stock = 50,
        ISBNNO = "9780743273565",
        Publisher = "Scribner",
        Author = "F. Scott Fitzgerald",
        ImageUrl = "https://placehold.co/602x402"
    },
    new Book
    {
        BookId = 2,
        Name = "To Kill a Mockingbird",
        CategoryName = "Fiction",
        UnitPrice = 10.50,
        Stock = 70,
        ISBNNO = "9780061120084",
        Publisher = "Harper Perennial Modern Classics",
        Author = "Harper Lee",
        ImageUrl = "https://placehold.co/602x402"
    },
    new Book
    {
        BookId = 3,
        Name = "Harry Potter and the Sorcerer's Stone",
        CategoryName = "Fantasy",
        UnitPrice = 15.99,
        Stock = 60,
        ISBNNO = "9780590353427",
        Publisher = "Scholastic",
        Author = "J.K. Rowling",
        ImageUrl = "https://placehold.co/602x402"
    },
    new Book
    {
        BookId = 4,
        Name = "Pride and Prejudice",
        CategoryName = "Classic",
        UnitPrice = 9.99,
        Stock = 55,
        ISBNNO = "9780141439518",
        Publisher = "Penguin Classics",
        Author = "Jane Austen",
        ImageUrl = "https://placehold.co/602x402"
    },
    new Book
    {
        BookId = 5,
        Name = "The Catcher in the Rye",
        CategoryName = "Fiction",
        UnitPrice = 11.25,
        Stock = 40,
        ISBNNO = "9780316769488",
        Publisher = "Little, Brown and Company",
        Author = "J.D. Salinger",
        ImageUrl = "https://placehold.co/602x402"
    },
    new Book
    {
        BookId = 6,
        Name = "Lord of the Rings: The Fellowship of the Ring",
        CategoryName = "Fantasy",
        UnitPrice = 18.50,
        Stock = 65,
        ISBNNO = "9780618640157",
        Publisher = "Mariner Books",
        Author = "J.R.R. Tolkien",
        ImageUrl = "https://placehold.co/602x402"
    }
);

        }
    }
}
