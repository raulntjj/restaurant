using AutoMapper;
using RestaurantService.DTOs;
using RestaurantService.Models;

namespace RestaurantService.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantReadDTO>();
            CreateMap<RestaurantCreateDTO, Restaurant>();
        }
    }
}