﻿using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Models.DbModels
{
    public class ImageProductModel
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}
