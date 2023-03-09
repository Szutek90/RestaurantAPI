using Bogus;
using Bogus.Extensions;
using FluentValidation.Validators;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class BogusGenerator
    {
        public static List<Restaurant> Seed()
        {
            string locale = "pl";
            
            var addressGenerator = new Faker<Address>(locale)
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());

            var dishesGenerator = new Faker<Dish>(locale)
                .RuleFor(a => a.Name, f => f.Commerce.ProductName())
                .RuleFor(a => a.Price, f => f.Random.Int(5, 15))
                .RuleFor(a => a.Description, f => f.Lorem.Sentence(2));

            var restaurantsGenerator = new Faker<Restaurant>(locale)
                .RuleFor(u => u.Name, f => f.Company.CompanyName().ClampLength(max:20))
                .RuleFor(u => u.Description, f => f.Lorem.Sentence(3))
                .RuleFor(u => u.Category, f => f.Random.Word())
                .RuleFor(u => u.HasDeliviery, f => f.Random.Bool())
                .RuleFor(u => u.ContactEmail, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.ContactNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Address, f => addressGenerator.Generate())
                .RuleFor(u => u.Dishes, f => dishesGenerator.Generate(2));

            var restaurantsToAdd = restaurantsGenerator.Generate(10);
            return restaurantsToAdd;
            
            /*
            var restaurantsGenerator = new Faker<CreateRestaurantDto>(locale)
                .RuleFor(u => u.Name, f => f.Company.CompanyName())
                .RuleFor(u => u.Description, f => f.Lorem.Sentence(3))
                .RuleFor(u => u.Category, f => f.Random.Word())
                .RuleFor(u => u.HasDeliviery, f => f.Random.Bool())
                .RuleFor(u => u.ContactEmail, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.ContactNumber, f => f.Phone.PhoneNumber())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());
            var restaurantsToAdd = restaurantsGenerator.Generate(2);
            return restaurantsToAdd;
            */
        }
    }
}
