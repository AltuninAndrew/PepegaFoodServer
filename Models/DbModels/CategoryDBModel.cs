using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Models.DbModels
{
    public class CategoryDBModel
    {
        
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
