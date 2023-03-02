using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Exceptions;
using System.Security.Claims;

namespace RestaurantAPI.Authorizations
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            _logger = logger; ;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(x => x.Type == "DateOfBirth");
            if (dateOfBirthClaim is null) throw new BadRequestException("Not logged");
            var dateOfBirth = DateTime.Parse(dateOfBirthClaim.Value);
            var userName = context.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;
            _logger.LogInformation($"User [{userName}] with birth age [{dateOfBirth}] ");
            if (dateOfBirth.AddYears(requirement.MinimumAge)>=DateTime.Today) 
            {
                _logger.LogInformation("authorization failed");
                context.Fail();
            }
            else
            {
                _logger.LogInformation("Authorization suceed");
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
