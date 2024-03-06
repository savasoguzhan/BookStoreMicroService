using BookStore.Web.Models;

namespace BookStore.Web.Service.IService
{
    public interface IDiscountService
    {
        Task<ResponseDto?> GetDiscountAsync(string couponCode);
        Task<ResponseDto?> GetAllDiscountAsync();
        Task<ResponseDto?> GetDiscountByIdAsync(int id);
        Task<ResponseDto?> CreateDiscountAsync(DiscountDto discountDto);
        Task<ResponseDto?> UpdateDiscountAsync(DiscountDto discountDto);
        Task<ResponseDto?> DeleteDiscountAsync(int id);
    }
}
