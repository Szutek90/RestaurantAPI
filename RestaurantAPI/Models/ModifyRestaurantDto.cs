﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class ModifyRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDeliviery { get; set; }
    }
}
