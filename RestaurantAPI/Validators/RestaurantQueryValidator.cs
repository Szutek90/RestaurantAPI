using FluentValidation;
using RestaurantAPI.Models;

namespace RestaurantAPI.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSize = new int[] { 5, 10, 15 };

        public RestaurantQueryValidator()
        {
            RuleFor(e=>e.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(e => e.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                    context.AddFailure($"Page size must be in {string.Join(',', allowedPageSize)}");
            });
        }
    }
}
