using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Services.BookAPI.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ISBNNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CategoryName", "ISBNNO", "ImageUrl", "Name", "Publisher", "Stock", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", "Fiction", "9780743273565", "https://placehold.co/602x402", "The Great Gatsby", "Scribner", 50, 12.99 },
                    { 2, "Harper Lee", "Fiction", "9780061120084", "https://placehold.co/602x402", "To Kill a Mockingbird", "Harper Perennial Modern Classics", 70, 10.5 },
                    { 3, "J.K. Rowling", "Fantasy", "9780590353427", "https://placehold.co/602x402", "Harry Potter and the Sorcerer's Stone", "Scholastic", 60, 15.99 },
                    { 4, "Jane Austen", "Classic", "9780141439518", "https://placehold.co/602x402", "Pride and Prejudice", "Penguin Classics", 55, 9.9900000000000002 },
                    { 5, "J.D. Salinger", "Fiction", "9780316769488", "https://placehold.co/602x402", "The Catcher in the Rye", "Little, Brown and Company", 40, 11.25 },
                    { 6, "J.R.R. Tolkien", "Fantasy", "9780618640157", "https://placehold.co/602x402", "Lord of the Rings: The Fellowship of the Ring", "Mariner Books", 65, 18.5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
