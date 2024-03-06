using BookStore.Web.Models;
using BookStore.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> BookIndex()
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


        public async Task<IActionResult> BookCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> BookCreate(BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _bookService.CreateBookAsync(bookDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "New Book Addedd";
                    return RedirectToAction(nameof(BookIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(bookDto);
        }

        [HttpGet]
        public async Task<IActionResult> BookDelete(int bookId)
        {
            ResponseDto? response = await _bookService.GetBookByIdAsync(bookId);
            if (response != null && response.IsSuccess)
            {
                BookDto? bookDto = JsonConvert.DeserializeObject<BookDto>(Convert.ToString(response.Result));
                return View(bookDto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> BookDelete(BookDto bookDto)
        {
            ResponseDto? response = await _bookService.DeleteBookAsync(bookDto.BookId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Book Deleted !!";
                return RedirectToAction(nameof(BookIndex));
            }
            else
            {
                TempData["erorr"] = response?.Message;

            }
            return View(bookDto);
        }

        [HttpGet]
        public async Task<IActionResult> BookUpdate(int bookId)
        {
            ResponseDto? response = await _bookService.GetBookByIdAsync(bookId);
            if (response != null && response.IsSuccess)
            {
                BookDto? bookDto = JsonConvert.DeserializeObject<BookDto>(Convert.ToString(response.Result));
                return View(bookDto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> BookUpdate(BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _bookService.UpdateBookAsync(bookDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Book Updated!!!";
                    return RedirectToAction(nameof(BookIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(bookDto);
        }

    }
}
