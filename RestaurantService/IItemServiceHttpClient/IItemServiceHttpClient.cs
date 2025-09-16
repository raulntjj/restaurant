using RestaurantService.DTOs;

namespace RestaurantService.ItemServiceHttpClient
{
    public interface IItemServiceHttpClient
    {
        public void SendRestaurantToItemService(RestaurantReadDTO readDTO);
    }
}