using FluentValidation;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Validators
{
    public class RegisterUserDtoValidation : AbstractValidator<CreateUserDto>
    {
        public RegisterUserDtoValidation(RestaurantDbContext dbContext)
        {
            RuleFor(e=>e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.ConfirmPassword).Equal(x => x.Password);
            RuleFor(e => e.Password).MinimumLength(6);
            RuleFor(e => e.Email).Custom((value, context) =>
            {
                var ifExists = dbContext.Users.Any(e => e.Email == value);
                if (ifExists) context.AddFailure("Email", "Current Email is already in use");
            }
            );

        }
    }
}
