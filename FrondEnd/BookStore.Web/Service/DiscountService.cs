using BookStore.Web.Models;
using BookStore.Web.Service.IService;
using BookStore.Web.Utility;

namespace BookStore.Web.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IBaseService _baseService;

        public DiscountService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateDiscountAsync(DiscountDto discountDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data=discountDto,
                Url = SD.DiscountAPIBase + "/api/discount" 

            });
        }

        public async Task<ResponseDto?> DeleteDiscountAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.DiscountAPIBase + "/api/discount/" + id

            });
        }

        public async Task<ResponseDto?> GetAllDiscountAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.DiscountAPIBase + "/api/discount"

            });
        }

        public async Task<ResponseDto?> GetDiscountAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.DiscountAPIBase + "/api/discount/GetByCode/"+couponCode

            });
        }

        public async  Task<ResponseDto?> GetDiscountByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.DiscountAPIBase + "/api/discount/"+id

            });
        }

        public async Task<ResponseDto?> UpdateDiscountAsync(DiscountDto discountDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = discountDto,
                Url = SD.DiscountAPIBase + "/api/discount"

            });
        }
    }
}
