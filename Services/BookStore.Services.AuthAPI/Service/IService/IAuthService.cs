﻿using BookStore.Services.AuthAPI.Models.Dto;

namespace BookStore.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDto registerationRequestDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignRole(string email, string roleName);
    }
}
