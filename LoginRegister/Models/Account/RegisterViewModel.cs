using System.ComponentModel.DataAnnotations;


namespace Oblivion.Models
{
  

    public class RegisterViewModel
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings =false)]
        [DataType(DataType.Password)]     
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        //public string ConfirmPassword { get; set; }
    }
}
