using BookStore.Services.ShoppingCartAPI.Models.Dto;
using BookStore.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace BookStore.Services.ShoppingCartAPI.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IHttpClientFactory _clientFactory;

        public DiscountService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<DiscountDto> GetDiscount(string discountCode)
        {
            var client = _clientFactory.CreateClient("Discount");
            var response = await client.GetAsync($"/api/discount/GetByCode/{discountCode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<DiscountDto>(Convert.ToString(resp.Result));
            }
            return new DiscountDto();
        }
    }
}
