using BookStore.Web.Models;

namespace BookStore.Web.Service.IService
{
    public interface IBookService
    {
        
        Task<ResponseDto?> GetAllBookAsync();
        Task<ResponseDto?> GetBookByIdAsync(int id);
        Task<ResponseDto?> CreateBookAsync(BookDto bookDto);
        Task<ResponseDto?> UpdateBookAsync(BookDto bookDto);
        Task<ResponseDto?> DeleteBookAsync(int id);
    }
}

