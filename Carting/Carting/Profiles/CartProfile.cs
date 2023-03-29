using AutoMapper;
using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.DAL.Models;

namespace CartingService.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>()
    .               ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<CartItem, CartItemDto>();
            CreateMap<CartItemDto, CartItem>();
        }
    }
}
