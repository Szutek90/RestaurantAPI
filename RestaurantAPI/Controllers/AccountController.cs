using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService=accountService;
        }

        [HttpPost("register")]
        public ActionResult Create([FromBody] CreateUserDto dto)
        {
            _accountService.Create(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            var token = _accountService.GenerateJwtToken(dto);
            return Ok(token);
        }
    }
}
