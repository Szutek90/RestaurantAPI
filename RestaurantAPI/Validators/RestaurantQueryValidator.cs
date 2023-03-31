using FluentValidation;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSize = new int[] { 5, 10, 15 };
        private string[] allowedDirections = {nameof(Restaurant.Name), nameof(Restaurant.Category),
            nameof(Restaurant.Description)};

        public RestaurantQueryValidator()
        {
            RuleFor(e=>e.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(e => e.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                    context.AddFailure($"Page size must be in {string.Join(',', allowedPageSize)}");
            });

            RuleFor(e=>e.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedDirections.Contains(value))
                .WithMessage($"Not allowed sorted column. Column must be {string.Join(", ", allowedDirections)}");
        }
    }
}
