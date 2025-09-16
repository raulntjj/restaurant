using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ItemService.Data;
using ItemService.DTOs;

namespace ItemService.Controllers;

[Route("api/items/restaurant")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public RestaurantController(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestaurantReadDTO>> GetRestaurants()
    {
        var restaurants = _repository.GetAllRestaurants();

        return Ok(_mapper.Map<IEnumerable<RestaurantReadDTO>>(restaurants));
    }
}