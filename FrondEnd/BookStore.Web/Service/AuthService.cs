using BookStore.Web.Models;
using BookStore.Web.Models.AuthDtos;
using BookStore.Web.Service.IService;
using BookStore.Web.Utility;

namespace BookStore.Web.Service
{
	public class AuthService : IAuthService
	{
		private readonly IBaseService _baseService;

		public AuthService(IBaseService baseService)
        {
			_baseService = baseService;
		}

		public async Task<ResponseDto?> AssingRoleAsync(RegisterationRequestDto registerationRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType=SD.ApiType.POST,
				Data= registerationRequestDto,
				Url=SD.AuthAPIBase+ "/api/auth/AssignRole"

			});
		}

		public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = loginRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/login"

			},withBearer: false);
		}

		public  async Task<ResponseDto?> RegisterAsync(RegisterationRequestDto registerationRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = registerationRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/register"

			}, withBearer: false);
		}
	}
}
