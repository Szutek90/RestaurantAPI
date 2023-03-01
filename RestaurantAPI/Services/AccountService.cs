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
        private readonly AuthenticationSettings _authentication;

        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authentication)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authentication = authentication;
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

        public string GenerateJwtToken(LoginUserDto dto)
        {
            var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(e => e.Email == dto.EMail);
            if (user is null) throw new BadRequestException("Invalid username or password");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed) throw new BadRequestException("Invalid username or password");
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
            };
            if (!string.IsNullOrEmpty(user.Nationality))
                claims.Add(new Claim("Nationality", user.Nationality));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.JwtKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authentication.JwtExpireDays);
            var token = new JwtSecurityToken(_authentication.JwtIssuer, _authentication.JwtIssuer,
                claims, expires:expires, signingCredentials:credential);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
