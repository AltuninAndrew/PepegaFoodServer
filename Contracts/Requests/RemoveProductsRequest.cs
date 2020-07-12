using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Contracts.Requests
{
    public class RemoveProductsRequest
    {
        public int[] ProductsId { get; set; }

    }
}
