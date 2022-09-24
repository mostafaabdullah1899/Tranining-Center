using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
