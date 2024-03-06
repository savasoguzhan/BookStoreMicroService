using BookStore.Services.DiscountAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.DiscountAPI.Data
{
    public class UygulamaDbContext : DbContext
    {

        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options):base(options)
        {
            
        }

        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discount>().HasData(new Discount
            {
                DiscountId=1,
                CouponCode="10OFF",
                DiscountAmount=10,
                MinAmount=20
            });

            modelBuilder.Entity<Discount>().HasData(new Discount
            {
                DiscountId = 2,
                CouponCode = "20OFF",
                DiscountAmount = 20,
                MinAmount = 40
            });
        }
    }
}
