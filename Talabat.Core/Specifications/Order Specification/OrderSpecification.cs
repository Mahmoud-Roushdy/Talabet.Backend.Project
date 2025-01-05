using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_Specification
{
    public class OrderSpecification : BaseSpecifications<Order>
    {
        public OrderSpecification(string BuyerEmail)
        :base (O => O.BuyerEmail == BuyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDesc(O => O.OrderDate);
        }
        public OrderSpecification(int OrderId,string BuyerEmail)
       : base(O => O.BuyerEmail == BuyerEmail && 
                O.Id == OrderId
          ) 
          
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
           
        }
    }
}
