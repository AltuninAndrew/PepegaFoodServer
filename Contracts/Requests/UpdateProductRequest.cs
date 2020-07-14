using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Contracts.Requests
{
    public class UpdateProductRequest
    {
        [Required]
        public int ProductId { get; set; }

        public string NewName { get; set; }

        public string NewImageProductUrl { get; set; }

        public float? NewCount { get; set; }

        public string NewCountType { get; set; }

        public string NewCategory { get; set; }

        public float? NewPrimaryPrice { get; set; }
        
        public float? NewSecondaryPrice { get; set; }
    }
}
