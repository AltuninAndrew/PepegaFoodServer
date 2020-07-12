using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Contracts.Requests
{
    public class ChangeFirstNameRequest
    {
        [MinLength(2, ErrorMessage = "First name lenght should be more then 1 chars")]
        [MaxLength(50, ErrorMessage = "Firs name lenght should be less then 50 char")]
        public string NewFirstName { get; set; }
    }
}
