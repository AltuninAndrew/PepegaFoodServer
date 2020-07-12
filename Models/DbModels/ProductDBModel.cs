using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PepegaFoodServer.Models.DbModels
{
    public class ProductDBModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public ImageProductDBModel ImageProductUrl { get; set; }

        [Required]
        public float Count { get; set; }

        [Required]
        public int CounteTypeId { get; set; }

        [ForeignKey(nameof(CounteTypeId))]
        public CountTypeDBModel CountType { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CategoryDBModel Category { get; set; }

        [Required]
        public float PrimaryPrice { get; set; }

        public float SecondaryPrice { get; set; }
    }
}
