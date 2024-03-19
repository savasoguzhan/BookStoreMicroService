using AutoMapper;
using BookStore.Services.ShoppingCartAPI.Data;
using BookStore.Services.ShoppingCartAPI.Models;
using BookStore.Services.ShoppingCartAPI.Models.Dto;
using BookStore.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace BookStore.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UygulamaDbContext _dbContext;
        private readonly IBookService _bookService;
        private readonly IDiscountService _discountService;
        private readonly ILogger<CartAPIController> _logger;
        private ResponseDto _response;

        public CartAPIController(IMapper mapper, UygulamaDbContext dbContext, IBookService bookService,IDiscountService discountService,ILogger<CartAPIController> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _bookService = bookService;
            _discountService = discountService;
            _logger = logger;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
             var books= await _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_dbContext.CartHeaders.First(u =>u.UserId==userId))
                };
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_dbContext.CartDetails
                    .Where(u =>u.CartHeaderId==cart.CartHeader.CartHeaderId));

                //load all books here for cart
                IEnumerable<BookDto> bookDto = await _bookService.GetBooks();

                foreach (var item in cart.CartDetails)
                {
                    item.Book = bookDto.FirstOrDefault(x => x.BookId == item.BookId);
                    cart.CartHeader.CartTotal += (item.Count * item.Book.UnitPrice);
                }
                // apply discount if any 

                if (!string.IsNullOrEmpty(cart.CartHeader.DiscountCode))
                {
                    DiscountDto discount = await _discountService.GetDiscount(cart.CartHeader.DiscountCode);
                    if(discount != null && cart.CartHeader.CartTotal > discount.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= discount.DiscountAmount;
                        cart.CartHeader.Discount = discount.DiscountAmount;
                    }
                }

                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError($"Get Cart  Request failed : {ex.Message}");

            }
            _logger.LogError($"Get Cart Request Successfully : {_response.IsSuccess}");
            return _response;
        }


        [HttpPost("ApplyDiscount")]
        public async Task<object> AppyDiscount([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb =  await _dbContext.CartHeaders.FirstAsync(x => x.UserId == cartDto.CartHeader.UserId);
                cartFromDb.DiscountCode = cartDto.CartHeader.DiscountCode;
                _dbContext.CartHeaders.Update(cartFromDb);
                await  _dbContext.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError($"Apply Discount  Request failed : {ex.Message}");


            }
            _logger.LogError($"Apply Discount Request Successfully : {_response.IsSuccess}");
            return _response;
        }


       


        [HttpPost("CartUpsert")]
        public async  Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var bookStock = await _bookService.GetBooks();
                var cartHeaderFromDb = await _dbContext.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId ==cartDto.CartHeader.UserId );
                if ( cartHeaderFromDb == null )
                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _dbContext.CartHeaders.Add(cartHeader);
                    await _dbContext.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId=cartHeader.CartHeaderId;
                    _dbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    //Stock Control
                    foreach(var cartDetail in cartDto.CartDetails)
                    {
                        var book = bookStock.FirstOrDefault(x =>x.BookId == cartDetail.BookId);
                        if(cartDetail.Count > book.Stock)
                        {
                            _response.IsSuccess = false;
                            _response.Message = $"There is no stock for the book {book.Name}.";
                            return _response;
                        }
                    }
                    //check if details has same book
                    var cartDetailsFromDb = await _dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.BookId == cartDto.CartDetails.First().BookId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if(cartDetailsFromDb == null)
                    {
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _dbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                    
                    else
                    {
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _dbContext.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                }
                _response.Result= cartDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cart Upsert  Request failed : {ex.Message}");
                _response.Message = ex.Message.ToString();
                _response.IsSuccess= false;
            }
            _logger.LogError($"Cart Upsert  Request Successfully : {_response.IsSuccess}");
            return _response;
        }


        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody]int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _dbContext.CartDetails.First(u =>u.CartDetailsId==cartDetailsId);

                int totalCountOfCartItem = _dbContext.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                _dbContext.Remove(cartDetails);

                if(totalCountOfCartItem == 1)
                {
                    var cartHeaderToRemove = await _dbContext.CartHeaders.FirstOrDefaultAsync(u =>u.CartHeaderId == cartDetails.CartHeaderId);

                    _dbContext.Remove(cartHeaderToRemove);
                }


                await _dbContext.SaveChangesAsync();




                _response.Result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Cart Request failed : {ex.Message}");
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            _logger.LogError($"Remove Cart  Request Successfully : {_response.IsSuccess}");
            return _response;
        }

    }
}
