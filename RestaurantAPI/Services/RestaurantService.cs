using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(IMapper mapper, RestaurantDbContext dbContext, ILogger<RestaurantService> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
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
            if(restaurant is null) throw new NotFoundException("Not found entity to delete");
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

        public void Delete(int id)
        {
            _logger.LogError($"Entity with id {id} is going to be deleted");
            var restaurant = _dbContext.Restaurants.FirstOrDefault(e => e.Id == id);
            if (restaurant is null) throw new NotFoundException("Not found entity to delete");

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void Modify(ModifyRestaurantDto dto, int id)
        {
            var restaurantToModify = _dbContext.Restaurants.FirstOrDefault(e=>e.Id == id);
            if(restaurantToModify is null) throw new NotFoundException("Not found entity to modify");
            restaurantToModify.Name= dto.Name;
            restaurantToModify.Description= dto.Description;
            restaurantToModify.HasDeliviery= dto.HasDeliviery;
            _dbContext.SaveChanges();
        }
    }


}
