using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Models.DbModels
{
    public class ProductToShopModel
    {

        public int ProductId { get; set; }

        public int ShopId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductModel Client { get; set; }

        [ForeignKey(nameof(ShopId))]
        public ShopModel Shop { get; set; }
    }
}
