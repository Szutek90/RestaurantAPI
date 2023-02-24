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
            if(!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            int id = _restaurantService.Create(dto);

            return Created($"api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _restaurantService.Delete(id);
            if(result is false) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var restaurantsDto = _restaurantService.GetAll();
            return Ok(restaurantsDto);
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute]int id) 
        { 
            var restaurantDto = _restaurantService.GetById(id);

            if (restaurantDto == null)
            {
                return NotFound();
            }
            return Ok(restaurantDto);
        }
    }
}
