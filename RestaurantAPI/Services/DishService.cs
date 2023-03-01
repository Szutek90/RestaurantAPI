using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int AddDish(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishToAdd= _mapper.Map<Dish>(dto);
            dishToAdd.RestaurantId = restaurantId;
            _dbContext.Dishes.Add(dishToAdd);
            _dbContext.SaveChanges();

            return dishToAdd.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = restaurant.Dishes.FirstOrDefault(e=>e.Id== dishId);
            if (dish == null ) throw new NotFoundException("Not found equal dish by given ID");
            return _mapper.Map<DishDto>(dish);
        }

        public IEnumerable<DishDto> GetAll(int id) 
        {
            var restaurant = GetRestaurantById(id);
            return _mapper.Map<List<DishDto>>(restaurant.Dishes);
        }

        public Restaurant GetRestaurantById(int id)
        {
            var restaurant = _dbContext.Restaurants.Include(e=>e.Dishes).FirstOrDefault(e=>e.Id== id);
            if (restaurant is null) throw new NotFoundException("Not found equal restaurant by given ID");
            return restaurant;
        }

        public void Delete(int restaurantId, int dishId)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);
            var dish = restaurant.Dishes.FirstOrDefault(e => e.Id == dishId);
            if (dish == null) throw new NotFoundException("Not found equal dish by given ID");
            _dbContext.Dishes.Remove(dish);
            _dbContext.SaveChanges();
        }

        public void DeleteAll(int restaurantId)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);
            _dbContext.RemoveRange(restaurant.Dishes);
            _dbContext.SaveChanges();
        }
    }
}
