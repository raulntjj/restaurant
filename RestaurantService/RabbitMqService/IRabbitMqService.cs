using RestaurantService.DTOs;

namespace RestaurantService.RabbitMqClient
{
    public interface IRabbitMqClient
    {
        void PublishRestaurant(RestaurantReadDTO restaurantReadDTO);
    }
}