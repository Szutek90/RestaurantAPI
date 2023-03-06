using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorizations
{
    public enum ResourceOperation{
        Create, Read, Update, Delete
    }

    public class ResourceOperationRequirement :IAuthorizationRequirement
    {
        public ResourceOperation _resourceOperation { get; set; }
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            _resourceOperation = resourceOperation;
        }
    }
}
