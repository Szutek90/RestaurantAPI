using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class AccountService: IAccountService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void Create(CreateUserDto dto)
        {
            User userToAdd = new User()
            {
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };
            userToAdd.PasswordHash = _passwordHasher.HashPassword(userToAdd, dto.Password);
            _dbContext.Users.Add(userToAdd);
            _dbContext.SaveChanges();
        }
    }
}
