using System.Collections.Generic;

namespace PepegaFoodServer.Models
{
    public class AuthResultModel
    {

        public string Token { get; set; }
        public bool Success { get; set; }
        public string UserName { get; set; }

        public IEnumerable<string> ErrorsMessages { get; set; }
    }
}
