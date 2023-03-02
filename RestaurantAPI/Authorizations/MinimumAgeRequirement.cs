﻿using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorizations
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
