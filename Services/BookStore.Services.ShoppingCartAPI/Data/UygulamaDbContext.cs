using BookStore.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.ShoppingCartAPI.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options):base(options)
        {
            
        }

        public DbSet<CartHeader> CartHeaders { get; set; }

        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
