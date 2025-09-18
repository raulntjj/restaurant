using ItemService.Models;

namespace ItemService.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateItem(int restaurantId, Item item)
        {
            item.RestaurantId = restaurantId;
            _context.Items.Add(item);
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
        }

        public bool ExternalRestaurantExists(int externalRestaurantId)
        {
            return _context.Restaurants.Any(restaurant => restaurant.ExternalId == externalRestaurantId);
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        public Item GetItem(int restaurantId, int itemId) => _context.Items
            .Where(item => item.RestaurantId == restaurantId && item.Id == itemId).FirstOrDefault();

        public IEnumerable<Item> GetRestaurantItems(int restaurantId)
        {
            return _context.Items
                .Where(item => item.RestaurantId == restaurantId);
        }

        public bool RestaurantExists(int restaurantId)
        {
            return _context.Restaurants.Any(restaurant => restaurant.Id == restaurantId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}