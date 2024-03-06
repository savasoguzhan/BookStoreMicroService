using BookStore.Services.ShoppingCartAPI.Models.Dto;

namespace BookStore.Services.ShoppingCartAPI.Service.IService
{
    public interface IDiscountService
    {
        Task<DiscountDto> GetDiscount(string discountCode);
    }
}
