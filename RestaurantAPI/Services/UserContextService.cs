using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IUserContextService
    {
        public ClaimsPrincipal User { get; }
        public int UserId { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserContextService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public ClaimsPrincipal User => _accessor.HttpContext?.User;
        public int UserId => int.Parse(_accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);

    }
}
