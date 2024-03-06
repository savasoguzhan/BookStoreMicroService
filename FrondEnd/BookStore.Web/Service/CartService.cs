using BookStore.Web.Models;
using BookStore.Web.Service.IService;
using BookStore.Web.Utility;

namespace BookStore.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyDiscountAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = cartDto,
                Url= SD.CartAPIBase + "/api/cart/ApplyDiscount"
            });
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.CartAPIBase + "/api/cart/GetCart/"+userId
            });
        }

        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = cartDetailsId,
                Url = SD.CartAPIBase + "/api/cart/RemoveCart"
            });
        }

        public async  Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = cartDto,
                Url = SD.CartAPIBase + "/api/cart/CartUpsert"
            });
        }
    }
}
