using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Services.ShoppingCartAPI.Models.Dto
{
    public class CartDetailsDto
    {
        
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }

        
        public CartHeader? CartHeader { get; set; }

        public int BookId { get; set; }
        
        public BookDto? Book { get; set; }

        public int Count { get; set; }
    }
}
