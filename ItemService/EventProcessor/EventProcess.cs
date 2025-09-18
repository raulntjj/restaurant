using System.Net.Sockets;
using System.Text.Json;
using AutoMapper;
using ItemService.Data;
using ItemService.DTOs;
using ItemService.Models;

namespace ItemService.EventProcessor;

public class EventProcess : IEventProcess
{
  private readonly IMapper _mapper;
  private readonly IServiceScopeFactory _scope;

  public EventProcess(IMapper mapper)
  {
    _mapper = mapper;
  }

  public void Process(string message)
  {
    using var scope = _scope.CreateScope();

    var itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();

    var restaurantReadDTO = JsonSerializer.Deserialize<RestaurantReadDTO>(message);
    var restaurant = _mapper.Map<Restaurant>(restaurantReadDTO);

    if (!itemRepository.ExternalRestaurantExists(restaurant.Id))
    {
      itemRepository.CreateRestaurant(restaurant);
      itemRepository.SaveChanges();
    }
  }
}