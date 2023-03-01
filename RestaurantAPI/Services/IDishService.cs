using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int AddDish(int restaurantId, CreateDishDto dto);
        DishDto GetById(int restaurantId, int dishId);
        IEnumerable<DishDto> GetAll(int id);
    }
}