using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Contracts.Requests
{
    public class ChangeEmailRequest
    {
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
