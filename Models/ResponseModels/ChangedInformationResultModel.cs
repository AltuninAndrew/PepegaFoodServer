using System.Collections.Generic;

namespace PepegaFoodServer.Models
{
    public class ChangedInformationResultModel
    {
        public bool Success { get; set; }
        public IEnumerable<string> ErrorsMessages { get; set; }
    }
}
