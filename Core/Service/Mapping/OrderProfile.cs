using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDtos;
using Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<AddressDto,OrderAddress>().ReverseMap();
            CreateMap<Order,OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, O =>O.MapFrom(S=>S.DeliveryMethod.ShortName))
                .ForMember(D => D.OrderStatus, O => O.MapFrom(S => S.Status));
            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(D => D.PictureUrl , O =>O.MapFrom<OrderItemPictureUrlResolver>())
                .ForMember(D => D.ProductName , O =>O.MapFrom(S=>S.product.ProductName) );
            CreateMap<DeliveryMethod, DeliveryMetodDto>();
        }

    }
}
