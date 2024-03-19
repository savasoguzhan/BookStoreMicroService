﻿
using Microsoft.AspNetCore.Authentication;

namespace BookStore.Services.ShoppingCartAPI.Utililty
{
    public class BackendApiAuthHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BackendApiAuthHttpClientHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
