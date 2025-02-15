﻿using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.Service.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public int DeliveryMethodId { get; set; } = 1;
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public decimal ShippingCost { get; set; }

    }
}
