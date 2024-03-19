using BookStore.Web.Models;
using BookStore.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartDetailId)
        {
            var userId = User.Claims
               .Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            ResponseDto? response = await _cartService.RemoveFromCartAsync(cartDetailId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart Removed";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(CartDto cartDto)
        {
            
            ResponseDto? response = await _cartService.ApplyDiscountAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Discount applied";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDiscount(CartDto cartDto)
        {
            cartDto.CartHeader.DiscountCode = "";

            ResponseDto? response = await _cartService.ApplyDiscountAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Discount applied";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }


        private async Task<CartDto> LoadCartDtoLoggedInUser()
        {
            var userId = User.Claims
                .Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            ResponseDto? response = await _cartService.GetCartByUserIdAsync(userId);

            if (response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }

            return new CartDto();


        }
    }
}
