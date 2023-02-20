using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMapper : Profile
    {
        public RestaurantMapper()
        {
            CreateMap<Restaurant, RestaurantDto>().ForMember(dest => dest.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(e => e.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(e => e.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(dest => dest.Address, act => act.MapFrom(src => new Address()
                { City = src.City, PostalCode = src.PostalCode, Street = src.Street }));
        }
    }
}
