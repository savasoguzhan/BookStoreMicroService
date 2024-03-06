using BookStore.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.AuthAPI.Data
{
    public class UygulamaDbContext : IdentityDbContext<ApplicationUser>
    {

        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options):base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
