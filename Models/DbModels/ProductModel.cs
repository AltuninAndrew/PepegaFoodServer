using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Models.DbModels
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public ImageProductModel ImageProductUrl { get; set; }

        [Required]
        public float Count { get; set; }

        [Required]
        public int CounteTypeId { get; set; }

        [ForeignKey(nameof(CounteTypeId))]
        public CountTypeModel CountType { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CategoryModel Category { get; set; }

        [Required]
        public float PrimaryPrice { get; set; }

        public float SecondaryPrice { get; set; }
    }
}
