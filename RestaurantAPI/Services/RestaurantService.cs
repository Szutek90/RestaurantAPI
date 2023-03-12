using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorizations;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(IMapper mapper, RestaurantDbContext dbContext, ILogger<RestaurantService> logger,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }


        public int Create(CreateRestaurantDto dto)
        {
            var restaurantToAdd = _mapper.Map<Restaurant>(dto);
            restaurantToAdd.CreatedById = _userContextService.UserId;
            _dbContext.Restaurants.Add(restaurantToAdd);
            _dbContext.SaveChanges();
            return restaurantToAdd.Id;
        }

        public PagedResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var baseQuery = _dbContext.Restaurants.Include(e => e.Address).Include(e => e.Dishes)
                .Where(e => query.searchPhrase == null || (e.Name.ToLower().Contains(query.searchPhrase.ToLower()) ||
                e.Description.ToLower().Contains(query.searchPhrase.ToLower())));

            var restaurants = baseQuery
                .Skip(query.PageSize * (query.PageNumber-1)).Take(query.PageSize).ToList();

            int totalItems = baseQuery.Count();

            var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);
            var pagedRestaurants =
                new PagedResult<RestaurantDto>(restaurantsDto, query.PageSize, query.PageNumber,totalItems);

            return pagedRestaurants;
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
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
            new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.Result.Succeeded)
                throw new ForbidException("You are not allowed to delete");
            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void Modify(ModifyRestaurantDto dto, int id)
        {
            
            var restaurantToModify = _dbContext.Restaurants.FirstOrDefault(e=>e.Id == id);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurantToModify,
                new ResourceOperationRequirement(ResourceOperation.Update));
            if (!authorizationResult.IsCompletedSuccessfully)
                throw new ForbidException("You are not allowed to do changes");
            if (restaurantToModify is null) throw new NotFoundException("Not found entity to modify");
            restaurantToModify.Name= dto.Name;
            restaurantToModify.Description= dto.Description;
            restaurantToModify.HasDeliviery= dto.HasDeliviery;
            _dbContext.SaveChanges();
        }

        public void BogusGenerate()
        {
            var restaurants = BogusGenerator.Seed();
            _dbContext.AddRange(restaurants);
            _dbContext.SaveChanges();
           
        }
    }


}
