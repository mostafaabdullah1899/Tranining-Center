



using Microsoft.AspNetCore.Identity;

namespace MVC_Project.Models
{
    //InCase IdentityUser Id=>String
    //InCase  IdentityUser<int> Id=>int
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; } 
    }
}
