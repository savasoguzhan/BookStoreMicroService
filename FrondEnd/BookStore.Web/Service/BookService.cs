using BookStore.Web.Models;
using BookStore.Web.Service.IService;
using BookStore.Web.Utility;

namespace BookStore.Web.Service
{
    public class BookService : IBookService
    {
        private readonly IBaseService _baseService;

        public BookService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateBookAsync(BookDto bookDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType=SD.ApiType.POST,
                Data = bookDto,
                Url=SD.BookAPIBase + "/api/book"
            });
        }

        public async Task<ResponseDto?> DeleteBookAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                
                Url = SD.BookAPIBase + "/api/book/" + id
            });
        }

        public async Task<ResponseDto?> GetAllBookAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BookAPIBase + "/api/book"

            });
        }

        public async Task<ResponseDto?> GetBookByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BookAPIBase + "/api/book/" + id

            });
        }

        public async Task<ResponseDto?> UpdateBookAsync(BookDto bookDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = bookDto,
                Url = SD.BookAPIBase + "/api/book"
            });
        }
    }
}
