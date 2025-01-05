using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Order_Aggregate;

namespace Talabat.Core.Services
{
    public  interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodId, Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail); 
        Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethods();

    }
}
