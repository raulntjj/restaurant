using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.Data;
using RestaurantService.DTOs;
// using RestaurantService.ItemServiceHttpClient;
using RestaurantService.Models;
using RestaurantService.RabbitMqClient;

namespace RestaurantService.Controllers;

[Route("api/restaurants")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _repository;
    private readonly IMapper _mapper;
    // private IItemServiceHttpClient _itemServiceHttpClient;
    private IRabbitMqClient _rabbitMqClient;

    public RestaurantController(
        IRestaurantRepository repository,
        IMapper mapper,
        // IItemServiceHttpClient itemServiceHttpClient
        IRabbitMqClient rabbitMqClient
    )
    {
        _repository = repository;
        _mapper = mapper;
        // _itemServiceHttpClient = itemServiceHttpClient;
        _rabbitMqClient = rabbitMqClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestaurantReadDTO>> GetAllRestaurants()
    {

        var restaurants = _repository.GetAllRestaurants();

        return Ok(_mapper.Map<IEnumerable<RestaurantReadDTO>>(restaurants));
    }

    [HttpGet("{id}", Name = "GetRestaurantById")]
    public ActionResult<RestaurantReadDTO> GetRestaurantById(int id)
    {
        var restaurants = _repository.GetRestaurantById(id);
        if (restaurants != null)
        {
            return Ok(_mapper.Map<RestaurantReadDTO>(restaurants));
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantReadDTO>> CreateRestaurant(RestaurantCreateDTO RestaurantCreateDTO)
    {
        var restaurant = _mapper.Map<Restaurant>(RestaurantCreateDTO);
        _repository.CreateRestaurant(restaurant);
        _repository.SaveChanges();

        var RestaurantReadDTO = _mapper.Map<RestaurantReadDTO>(restaurant);

        // _itemServiceHttpClient.SendRestaurantToItemService(RestaurantReadDTO);

        _rabbitMqClient.PublishRestaurant(RestaurantReadDTO);


        return CreatedAtRoute(nameof(GetRestaurantById), new { RestaurantReadDTO.Id }, RestaurantReadDTO);
    }
}