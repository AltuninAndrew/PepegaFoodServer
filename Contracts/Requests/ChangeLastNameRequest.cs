using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Contracts.Requests
{
    public class ChangeLastNameRequest
    {
        [MinLength(2, ErrorMessage = "Last name lenght should be more then 1 chars")]
        [MaxLength(50, ErrorMessage = "Last name lenght should be less then 50 char")]
        public string NewLastName { get; set; }
    }
}
