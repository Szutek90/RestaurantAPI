using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        void Create(CreateUserDto dto);
    }
}
