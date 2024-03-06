using BookStore.Services.AuthAPI.Data;
using BookStore.Services.AuthAPI.Models;
using BookStore.Services.AuthAPI.Models.Dto;
using BookStore.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UygulamaDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UygulamaDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }


            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenereteToken(user,roles);

            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                ID = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                BrithData = user.BrithData

            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token

            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerationRequestDto.Email,
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                FullName = registerationRequestDto.FullName,
                PhoneNumber = registerationRequestDto.PhoneNumber,
                BrithData = registerationRequestDto.BrithData
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToreturn = _dbContext.ApplicationUsers.First(u => u.UserName == registerationRequestDto.Email);

                    UserDto userDto = new UserDto()
                    {
                        Email = userToreturn.Email,
                        ID = userToreturn.Id,
                        FullName = userToreturn.FullName,
                        PhoneNumber = userToreturn.PhoneNumber,
                        BrithData = userToreturn.BrithData

                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return "Error";

        }
    }
}
