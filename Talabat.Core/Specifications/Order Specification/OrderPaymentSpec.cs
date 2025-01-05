using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_Specification
{
    public class OrderPaymentSpec : BaseSpecifications<Order>
    {
        public OrderPaymentSpec(string PaymentId):base (O =>O.PaymentIndentId == PaymentId)
        {
            
        }
    }
}
