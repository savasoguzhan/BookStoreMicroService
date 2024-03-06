using BookStore.Services.ShoppingCartAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {

        [Key]
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }

        public int BookId { get; set; }
        [NotMapped]
        public BookDto Book { get; set; }

        public int Count { get; set; }
    }
}
