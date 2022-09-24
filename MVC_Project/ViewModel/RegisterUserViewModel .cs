using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required]
        public string UserName { get; set; } //From IdentityUser
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }//From IdentityUser
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConFirmPassword { get; set; } //Extra Info
        [Required]
        public string Address { get; set; } //From ApplicationUSer
    }
}
