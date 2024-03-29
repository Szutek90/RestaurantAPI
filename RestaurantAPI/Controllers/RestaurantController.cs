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
            int id = _restaurantService.Create(dto);
            
            return Created($"api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return Ok();
        }

        [Authorize(Policy = "TwoRestaurants")]
        [HttpGet]
        public ActionResult<PagedResult<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query)
        {
            var restaurantsDto = _restaurantService.GetAll(query);
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
            _restaurantService.Modify(dto, id);
            return Ok();
        }

        [HttpGet("bogus")]
        [Authorize(Roles= "Admin")]
        public ActionResult BogusSeed()
        {
            _restaurantService.BogusGenerate();
            return Ok();
        }
    }
}
