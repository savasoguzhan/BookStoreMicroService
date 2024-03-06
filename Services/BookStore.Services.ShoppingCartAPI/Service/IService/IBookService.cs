using BookStore.Services.ShoppingCartAPI.Models.Dto;

namespace BookStore.Services.ShoppingCartAPI.Service.IService
{
    public interface IBookService
    {

        Task<IEnumerable<BookDto>> GetBooks();
    }
}
