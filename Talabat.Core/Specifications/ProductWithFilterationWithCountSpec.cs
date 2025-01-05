using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public  class ProductWithFilterationWithCountSpec : BaseSpecifications<Product>
    {
        public ProductWithFilterationWithCountSpec(ProductSpecParams productSpecParams) : base(p =>
        (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search)) &&
                  (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId.Value) &&

                   (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId.Value)
          )
        {

        }
    }
}
