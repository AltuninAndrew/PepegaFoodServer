using System;
using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Models.DbModels
{
    public class CountTypeDBModel
    {
        [Key]
        public int CountTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
