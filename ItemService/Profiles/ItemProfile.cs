using AutoMapper;
using ItemService.DTOs;
using ItemService.Models;

namespace ItemService.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Restaurant, RestaurantReadDTO>();
            CreateMap<ItemCreateDTO, Item>();
            CreateMap<Item, ItemCreateDTO>();
        }
    }
}