using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private readonly RestaurantDbContext _dbContext;

        public RestaurantService(IMapper mapper, RestaurantDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public int Create(CreateRestaurantDto dto)
        {
            var restaurantToAdd = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurantToAdd);
            _dbContext.SaveChanges();
            return restaurantToAdd.Id;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext.Restaurants.Include(e => e.Address).Include(e => e.Dishes).ToList();
            var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDto;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext.Restaurants.Include(e => e.Address)
            .Include(e => e.Dishes).FirstOrDefault(x => x.Id == id);
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

        public bool Delete(int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(e => e.Id == id);
            if (restaurant is null) return false;

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
