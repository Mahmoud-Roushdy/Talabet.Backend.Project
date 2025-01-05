using Talabat.Core.Entities.Identity.Order_Aggregate;

namespace Talabat.Service.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
      
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIndentId { get; set; } = string.Empty;
        //[NotMapped]
        //public decimal Total => Subtotal + DeliveryMethod.Cost;
        //public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;

    }
}
