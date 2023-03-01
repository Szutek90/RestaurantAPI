using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI.Services
{
    public class AccountService: IAccountService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
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

        public string Login(LoginUserDto dto)
        {
            var user = _dbContext.Users.Include(e => e.Role).FirstOrDefault(u => u.Email == dto.Email);
            if (user is null) throw new BadRequestException("Username or password is incorrect");
            var passwordChech = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (passwordChech == PasswordVerificationResult.Failed) throw new BadRequestException("Username or password is incorrect");
            var tokenHolder = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName}"),
                new Claim(ClaimTypes.Surname, $"{user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };
            var token = new JwtSecurityToken(
                issuer: _authenticationSettings.JwtIssuer,
                audience: _authenticationSettings.JwtAudience,
                expires: DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays),
                claims: claims,
                signingCredentials: credentials);

            return tokenHolder.WriteToken(token);
        }
    }
}
