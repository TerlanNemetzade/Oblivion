using System.ComponentModel.DataAnnotations;

namespace Oblivion.Models
{
    public class LoginViewModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter password")]

        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}
