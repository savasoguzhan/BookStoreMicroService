﻿namespace BookStore.Web.Models
{
    public class DiscountDto
    {
        public int DiscountId { get; set; }

        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }

        public int MinAmount { get; set; }
    }
}
