namespace BookStore.Services.DiscountAPI.Model.Dto
{
    public class DiscountDto
    {
        public int DiscountId { get; set; }

        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }

        public int MinAmount { get; set; }
    }
}
