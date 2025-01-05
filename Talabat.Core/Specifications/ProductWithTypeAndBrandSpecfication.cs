using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithTypeAndBrandSpecfication : BaseSpecifications<Product>
    {
        public ProductWithTypeAndBrandSpecfication( ProductSpecParams productSpecParams)
            : base(p =>
                  (string.IsNullOrEmpty(productSpecParams.Search) ||p.Name.ToLower().Contains(productSpecParams.Search)) &&
                  (!productSpecParams.BrandId.HasValue|| p.ProductBrandId == productSpecParams.BrandId.Value) &&

                   (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId.Value)


            )
        { 

            Includes.Add(P=>P.ProductBrand);
            Includes.Add(P=>P.ProductType); 
            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDec":
                        AddOrderByDesc(P => P.Price);
                        break;
                   
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            ApplyPagination(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);
        }

        public ProductWithTypeAndBrandSpecfication(int id):base(p=>p.Id== id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }


    }
}
