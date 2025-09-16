using RestaurantService.DTOs;
using System.Text;
using System.Text.Json;

namespace RestaurantService.ItemServiceHttpClient
{
    public class ItemServiceHttpClient : IItemServiceHttpClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public ItemServiceHttpClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async void SendRestaurantToItemService(RestaurantReadDTO readDTO)
        {
            var requestContent = new StringContent
                (
                    JsonSerializer.Serialize(readDTO),
                    Encoding.UTF8,
                    "application/json"
                );

            await _client.PostAsync(_configuration["ItemService"], requestContent);
        }
    }
}