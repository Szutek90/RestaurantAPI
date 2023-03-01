using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any()) { 
                    _dbContext.Restaurants.AddRange(GetRestaurants());
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Tomcio Paluch",
                    Description = "Fast food trele morele opis restauracji",
                    Category = "Fastfood",
                    HasDeliviery= false,
                    ContactEmail = "tomcio@costam.pl",
                    ContactNumber = "123456789",
                    Address = new Address()
                    {
                        City = "Bialystok",
                        Street = "Raginisa 15001900",
                        PostalCode= "15-404"
                    },

                    Dishes = new List<Dish>{ new Dish()
                    {
                        Name = "Kebab w picie",
                        Price = 15,
                        Description = " Opis kebaba w picie"

                    },
                    new Dish(){
                        Name = "Frytki z batatow",
                        Price = 26,
                        Description = "Fryty z batatow bardzo dobre"
                    },
                    },
                },
                new Restaurant()
                {
                    Name = "Pizzeria Meduza",
                    Description = "The best pizza w łapach",
                    Category = "Pizza i fastfood",
                    HasDeliviery= true,
                    ContactEmail = "meduza@meduza.pl",
                    ContactNumber = "986356",
                    Dishes = new List<Dish>{ new Dish()
                    {
                        Name = "Margarita",
                        Price = 20,
                    },
                    new Dish()
                    {
                        Name = "Tortilla",
                        Price=15,
                    },
                    },
                    Address = new Address()
                    {
                        City="Łapy",
                        Street = "barwikowska",
                        PostalCode = "16-008"
                    }
                }
            };
            return restaurants;
        } 
    }
}
