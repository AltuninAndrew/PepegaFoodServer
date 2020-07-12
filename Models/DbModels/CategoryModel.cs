using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Models.DbModels
{
    public class CategoryModel
    {
        
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
