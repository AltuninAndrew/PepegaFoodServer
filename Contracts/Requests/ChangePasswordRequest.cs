using System.ComponentModel.DataAnnotations;

namespace PepegaFoodServer.Contracts.Requests
{
    public class ChangePasswordRequest
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
