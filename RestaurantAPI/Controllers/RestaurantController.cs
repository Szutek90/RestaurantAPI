using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto) 
        {
            int id = _restaurantService.Create(dto);

            return Created($"api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDto = _restaurantService.GetAll();
            return Ok(restaurantsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id) 
        { 
            var restaurantDto = _restaurantService.GetById(id);
            return Ok(restaurantDto);
        }

        [HttpPut("{id}")]
        public ActionResult ModifyRestaurant([FromBody] ModifyRestaurantDto dto, [FromRoute]int id)
        {
            _restaurantService.Modify(dto, id);
            return Ok();
        }
    }
}
