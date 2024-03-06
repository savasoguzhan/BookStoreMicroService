using Microsoft.AspNetCore.Identity;

namespace BookStore.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime BrithData { get; set; }
    }
}
