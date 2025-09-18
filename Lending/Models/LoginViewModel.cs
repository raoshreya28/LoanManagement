using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter the User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
