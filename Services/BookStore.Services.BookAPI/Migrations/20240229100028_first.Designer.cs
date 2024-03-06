﻿// <auto-generated />
using BookStore.Services.BookAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.Services.BookAPI.Migrations
{
    [DbContext(typeof(UygulamaDbContext))]
    [Migration("20240229100028_first")]
    partial class first
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookStore.Services.BookAPI.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBNNO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("BookId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "F. Scott Fitzgerald",
                            CategoryName = "Fiction",
                            ISBNNO = "9780743273565",
                            ImageUrl = "https://placehold.co/602x402",
                            Name = "The Great Gatsby",
                            Publisher = "Scribner",
                            Stock = 50,
                            UnitPrice = 12.99
                        },
                        new
                        {
                            BookId = 2,
                            Author = "Harper Lee",
                            CategoryName = "Fiction",
                            ISBNNO = "9780061120084",
                            ImageUrl = "https://placehold.co/602x402",
                            Name = "To Kill a Mockingbird",
                            Publisher = "Harper Perennial Modern Classics",
                            Stock = 70,
                            UnitPrice = 10.5
                        },
                        new
                        {
                            BookId = 3,
                            Author = "J.K. Rowling",
                            CategoryName = "Fantasy",
                            ISBNNO = "9780590353427",
                            ImageUrl = "https://placehold.co/602x402",
                            Name = "Harry Potter and the Sorcerer's Stone",
                            Publisher = "Scholastic",
                            Stock = 60,
                            UnitPrice = 15.99
                        },
                        new
                        {
                            BookId = 4,
                            Author = "Jane Austen",
                            CategoryName = "Classic",
                            ISBNNO = "9780141439518",
                            ImageUrl = "https://placehold.co/602x402",
                            Name = "Pride and Prejudice",
                            Publisher = "Penguin Classics",
                            Stock = 55,
                            UnitPrice = 9.9900000000000002
                        },
                        new
                        {
                            BookId = 5,
                            Author = "J.D. Salinger",
                            CategoryName = "Fiction",
                            ISBNNO = "9780316769488",
                            ImageUrl = "https://placehold.co/602x402",
                            Name = "The Catcher in the Rye",
                            Publisher = "Little, Brown and Company",
                            Stock = 40,
                            UnitPrice = 11.25
                        },
                        new
                        {
                            BookId = 6,
                            Author = "J.R.R. Tolkien",
                            CategoryName = "Fantasy",
                            ISBNNO = "9780618640157",
                            ImageUrl = "https://placehold.co/602x402",
                            Name = "Lord of the Rings: The Fellowship of the Ring",
                            Publisher = "Mariner Books",
                            Stock = 65,
                            UnitPrice = 18.5
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
