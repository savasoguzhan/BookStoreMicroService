using BookStore.Web.Models;
using BookStore.Web.Service.IService;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService, ICartService cartService )
        {
            _logger = logger;
            _bookService = bookService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<BookDto>? list = new();

            ResponseDto? response = await _bookService.GetAllBookAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<BookDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Detail(int bookId)
        {
            BookDto? book = new();

            ResponseDto? response = await _bookService.GetBookByIdAsync(bookId);
            if (response != null && response.IsSuccess)
            {
                book = JsonConvert.DeserializeObject<BookDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(book);
        }


        [Authorize]
        [HttpPost]
        [ActionName("Detail")]
        public async Task<IActionResult> Detail(BookDto bookDto)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type ==JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = bookDto.Count,
                BookId = bookDto.BookId,

            };

            List<CartDetailsDto> cartDetailsDtos = new() {cartDetails};

            cartDto.CartDetails = cartDetailsDtos;

            ResponseDto? response = await _cartService.UpsertCartAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Add to basket";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(cartDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
