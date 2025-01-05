using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Identity.Order_Aggregate;
using Talabat.Service.DTOs;
using Talabat.Service.Helpers;

namespace Talabat.Service.Mapping_Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProtuctToDto>()
                .ForMember(D => D.ProductBrand, o => o.MapFrom(P => P.ProductBrand.Name))
                .ForMember(D => D.ProductType, o => o.MapFrom(P => P.ProductType.Name))
                .ForMember(D => D.PictureUrl, o => o.MapFrom<PictureResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.Identity.Order_Aggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(O => O.DeliveryMethod, O => O.MapFrom(O => O.DeliveryMethod.ShortName))
                .ForMember(O => O.DeliveryMethodCost, O => O.MapFrom(O => O.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(M => M.ProductName, M => M.MapFrom(M => M.Proudct.ProductName))
                .ForMember(M => M.ProductId, M => M.MapFrom(M => M.Proudct.ProductId))
                .ForMember(M => M.PictureUrl, M => M.MapFrom(M => M.Proudct.PictureUrl));
              

                
                

        }
    } 
}
