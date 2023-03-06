using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using System.Security.Claims;

namespace RestaurantAPI.Authorizations
{
    public class TwoRestaurantsRequirementHandler : AuthorizationHandler<TwoRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _dbContext;

        public TwoRestaurantsRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TwoRestaurantsRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst(e => e.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null) throw new BadRequestException("Not logged");
            int userId = int.Parse(userIdClaim.Value);
            int counted = _dbContext.Restaurants.Count(e=>e.CreatedById== userId);
            if (counted >= requirement.Minimum)
            {
                context.Succeed(requirement);
            }
            else
                context.Fail();
            return Task.CompletedTask;
        }
    }
}
