using System.ComponentModel.DataAnnotations;

namespace BookStore.Services.DiscountAPI.Model
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }

        public int MinAmount { get; set; }

        

    }
}
