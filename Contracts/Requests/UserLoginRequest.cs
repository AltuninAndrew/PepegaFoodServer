using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Contracts.Requests
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
