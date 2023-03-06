using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorizations
{
    public class TwoRestaurantsRequirement: IAuthorizationRequirement
    {
        public int Minimum { get; }

        public TwoRestaurantsRequirement(int minimum)
        {
            Minimum = minimum;
        }
    }
}
