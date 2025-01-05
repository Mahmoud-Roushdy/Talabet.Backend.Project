using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Identity.Order_Aggregate;

namespace Talabat.Service.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto  ShippingAddress { get; set; }
    }
}
