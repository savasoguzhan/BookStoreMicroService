﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {

        
        public int CartHeaderId { get; set; }

        public string? UserId { get; set; }

        public string? DiscountCode { get; set; }


        
        public double Discount { get; set; }

        
        public double CartTotal { get; set; }
    }
}
