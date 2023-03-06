﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto) 
        {
            int userId = int.Parse(User.FindFirst(e => e.Type == ClaimTypes.NameIdentifier).Value);
            int id = _restaurantService.Create(dto, userId);
            
            return Created($"api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id, User);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDto = _restaurantService.GetAll();
            return Ok(restaurantsDto);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "HasNationality")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id) 
        { 
            var restaurantDto = _restaurantService.GetById(id);
            return Ok(restaurantDto);
        }

        [HttpPut("{id}")]
        public ActionResult ModifyRestaurant([FromBody] ModifyRestaurantDto dto, [FromRoute]int id)
        {
            _restaurantService.Modify(dto, id, User);
            return Ok();
        }
    }
}
