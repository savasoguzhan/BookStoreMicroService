using BookStore.Services.ShoppingCartAPI.Models.Dto;
using BookStore.Services.ShoppingCartAPI.Service.IService;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace BookStore.Services.ShoppingCartAPI.Service
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _clientFactory;

        public BookService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<BookDto>> GetBooks()
        {
            var cleint = _clientFactory.CreateClient("Book");

            var response = await cleint.GetAsync($"/api/book");

            var apiContet = await response.Content.ReadAsStringAsync();

            var resp= JsonConvert.DeserializeObject<ResponseDto>(apiContet);

            if(resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<BookDto>>(Convert.ToString(resp.Result));
            }
            return new List<BookDto>();
        }
    }
}
