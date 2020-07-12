using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Models.DbModels
{
    public class CountTypeModel
    {
        [Key]
        public int CountTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
