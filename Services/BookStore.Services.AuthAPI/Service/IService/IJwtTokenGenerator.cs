using BookStore.Services.AuthAPI.Models;

namespace BookStore.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {

        string GenereteToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
