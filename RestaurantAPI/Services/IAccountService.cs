using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        void Create(CreateUserDto dto);
        string Login(LoginUserDto dto);
        void Delete(int id);
    }
}
