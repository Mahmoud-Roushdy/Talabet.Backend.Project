using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProudctItemOrdered proudct, decimal price, int quantity)
        {
            Proudct = proudct;
            Price = price;
            Quantity = quantity;
        }

        public ProudctItemOrdered Proudct {  get; set; }    
        
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
