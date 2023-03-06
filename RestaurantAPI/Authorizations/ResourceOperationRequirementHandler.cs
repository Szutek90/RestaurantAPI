using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Security.Claims;

namespace RestaurantAPI.Authorizations
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Restaurant restaurant)
        {
            if(requirement._resourceOperation == ResourceOperation.Create ||
                requirement._resourceOperation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }
            int userId = int.Parse(context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (userId == restaurant.CreatedById)
            {
                context.Succeed(requirement);
            }
            else
                context.Fail();
            return Task.CompletedTask;
        }
    }
}
