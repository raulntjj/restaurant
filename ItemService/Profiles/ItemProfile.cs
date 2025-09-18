using AutoMapper;
using ItemService.DTOs;
using ItemService.Models;

namespace ItemService.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<RestaurantReadDTO, Restaurant>()
            .ForMember(destination => destination.ExternalId, options => options.MapFrom(source => source.Id));
            CreateMap<Restaurant, RestaurantReadDTO>();
            CreateMap<ItemCreateDTO, Item>();
            CreateMap<Item, ItemCreateDTO>();
        }
    }
}