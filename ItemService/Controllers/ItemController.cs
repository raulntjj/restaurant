using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ItemService.DTOs;
using ItemService.Data;
using ItemService.Models;

namespace ItemService.Controllers;

[Route("api/items/restaurants/{restaurantId}/item")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemController(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ItemReadDTO>> GetItemsForRestaurant(int restaurantId)
    {

        if (!_repository.RestaurantExists(restaurantId))
        {
            return NotFound();
        }

        var items = _repository.GetRestaurantItems(restaurantId);

        return Ok(_mapper.Map<IEnumerable<ItemReadDTO>>(items));
    }

    [HttpGet("{itemId}", Name = "GetItemForRestaurant")]
    public ActionResult<ItemReadDTO> GetItemForRestaurant(int restaurantId, int itemId)
    {
        if (!_repository.RestaurantExists(restaurantId))
        {
            return NotFound();
        }

        var item = _repository.GetItem(restaurantId, itemId);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ItemReadDTO>(item));
    }

    [HttpPost]
    public ActionResult<ItemReadDTO> CreateItemForRestaurant(int restaurantId, ItemCreateDTO itemDTO)
    {
        if (!_repository.RestaurantExists(restaurantId))
        {
            return NotFound();
        }

        var item = _mapper.Map<Item>(itemDTO);

        _repository.CreateItem(restaurantId, item);
        _repository.SaveChanges();

        var itemReadDTO = _mapper.Map<ItemReadDTO>(item);

        return CreatedAtRoute(nameof(GetItemForRestaurant),
            new { restaurantId, ItemId = itemReadDTO.Id }, itemReadDTO);
    }

}