using BookStore.Web.Models;
using BookStore.Web.Models.AuthDtos;
using BookStore.Web.Service.IService;
using BookStore.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStore.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
			_authService = authService;
            _tokenProvider = tokenProvider;
        }


		[HttpGet]
        public IActionResult Login()
		{
			LoginRequestDto loginRequestDto = new ();
			return View(loginRequestDto);
		}


		[HttpGet]
		public IActionResult Register()
		{
			var roleList = new List<SelectListItem>()
			{
				new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
				new SelectListItem{Text=SD.RoleUser, Value=SD.RoleUser},
			};


			ViewBag.RoleList = roleList;
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Register(RegisterationRequestDto registerationRequestDto)
        {
			ResponseDto responseDto = await _authService.RegisterAsync(registerationRequestDto);
			ResponseDto assignRole;
			if (responseDto != null && responseDto.IsSuccess)
			{
				if (string.IsNullOrEmpty(registerationRequestDto.Role))
				{
					registerationRequestDto.Role = SD.RoleUser;
				}
				assignRole = await _authService.AssingRoleAsync(registerationRequestDto);
				if(assignRole != null)
				{
					TempData["success"] = "Registration is Successful";
					return RedirectToAction(nameof(Login));
				}

			}
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleUser, Value=SD.RoleUser},
            };


            ViewBag.RoleList = roleList;

            return View(registerationRequestDto);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            ResponseDto  result = await _authService.LoginAsync(loginRequestDto);
			if(result != null && result.IsSuccess)
			{
				LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(result.Result));

				await SignInUser(loginResponseDto);
				_tokenProvider.SetToken(loginResponseDto.Token);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["error"] = result.Message;
				return View(loginRequestDto);
			}
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
			_tokenProvider.ClearToken();
			return RedirectToAction("Index", "Home");
        }

		private async Task SignInUser(LoginResponseDto model)
		{
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(ClaimTypes.Role,
               jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));




            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
