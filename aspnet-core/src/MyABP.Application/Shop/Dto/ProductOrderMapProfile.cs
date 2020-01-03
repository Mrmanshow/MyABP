using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Shop.Dto
{
    public class ProductOrderMapProfile: Profile
    {
        public ProductOrderMapProfile()
        {
            CreateMap<ProductOrder, ProductOrderDto>();
            CreateMap<ProductOrder, ProductOrderDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(i => i.Address.Name))
                .ForMember(x => x.DetailAddress, opt => opt.MapFrom(i => i.Address.DetailAddress))
                .ForMember(x => x.Phone, opt => opt.MapFrom(i => i.Address.Phone));
        }
    }
}
