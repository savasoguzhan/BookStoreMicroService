using BookStore.Web.Models;
using BookStore.Web.Models.AuthDtos;

namespace BookStore.Web.Service.IService
{
	public interface IAuthService
	{
		Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

		Task<ResponseDto?> RegisterAsync(RegisterationRequestDto registerationRequestDto);

		Task<ResponseDto?> AssingRoleAsync(RegisterationRequestDto registerationRequestDto);
	}
}
