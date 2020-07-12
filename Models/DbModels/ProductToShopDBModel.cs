using System.ComponentModel.DataAnnotations.Schema;

namespace PepegaFoodServer.Models.DbModels
{
    public class ProductToShopDBModel
    {

        public int ProductId { get; set; }

        public int ShopId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductDBModel Client { get; set; }

        [ForeignKey(nameof(ShopId))]
        public ShopDBModel Shop { get; set; }
    }
}
