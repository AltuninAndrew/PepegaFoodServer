
using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Models.DbModels
{
    public class ShopModel
    {
        [Key]
        public int ShopId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
