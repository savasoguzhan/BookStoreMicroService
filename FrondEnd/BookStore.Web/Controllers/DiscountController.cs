using BookStore.Web.Models;
using BookStore.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Web.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public async Task<IActionResult> DiscountIndex()
        {
            List<DiscountDto>? List = new();

            ResponseDto? response = await _discountService.GetAllDiscountAsync();
            if(response != null && response.IsSuccess)
            {
                List = JsonConvert.DeserializeObject<List<DiscountDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(List);
        }

        
    }
}
