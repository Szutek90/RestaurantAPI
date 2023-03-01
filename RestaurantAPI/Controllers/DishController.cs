using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("/api/restaurant/{restaurantId}/dish")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost]
        public ActionResult CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            int id = _dishService.AddDish(restaurantId, dto);
            return Created($"/api/restaurant/{restaurantId}/dish/{id}", null);
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> GetById([FromRoute]int restaurantId, [FromRoute] int dishId)
        {
            DishDto dish = _dishService.GetById(restaurantId, dishId);
            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<IEnumerable<DishDto>> GetAll([FromRoute]int restaurantId)
        {
            var dishes = _dishService.GetAll(restaurantId);
            return Ok(dishes);
        }

        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _dishService.Delete(restaurantId, dishId);
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute]int restaurantId)
        {
            _dishService.DeleteAll(restaurantId);
            return Ok();
        }
    }
}
