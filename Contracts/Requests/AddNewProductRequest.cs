using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Contracts.Requests
{
    public class AddNewProductRequest
    {

        [Required]
        [MinLength(2, ErrorMessage = "Name lenght should be more then 1 chars")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Should be more then 1 chars")]
        public string ImageProductUrl { get; set; }

        [Required]
        [Range(0, 1000000)]
        public float Count { get; set; }

        [Required]
        public string CountType { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(0, 1000000)]
        public float PrimaryPrice { get; set; }

        [Range(0, 1000000)]
        public float SecondaryPrice { get; set; }

        [MinLength(2, ErrorMessage = "Lenght should be more then 1 chars")]
        public string[] Shops { get; set; }
    }
}
