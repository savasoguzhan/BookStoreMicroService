using Azure;
using BookStore.Services.AuthAPI.Models.Dto;
using BookStore.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthAPIController> _logger;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService, ILogger<AuthAPIController> logger)
        {
            _authService = authService;
            _logger = logger;
            _responseDto = new ResponseDto();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _logger.LogError(errorMessage+ " new user cant  registered") ;
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
            _logger.LogInformation("A new user has registered");
            return Ok(_responseDto);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
        {

            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _logger.LogInformation("User Cant Login");
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorret";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginResponse;
            _logger.LogInformation("User Has Loggin");
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());

            if (!assignRoleSuccessful)
            {
                _logger.LogError($"Assign role failed: {model.Role}");
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ERROR";
                return BadRequest(_responseDto);
            }
            _logger.LogInformation("Assign to Role has been Succes ");
            return Ok(_responseDto);

        }
    }
}
