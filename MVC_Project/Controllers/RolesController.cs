using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.ViewModel;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    //3-Check Action Role
    //[Authorize] //login Valid Cookie 
    [Authorize(Roles="Admin")]       //login Valid Cookie contain Role=>Admin
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        //1-Create Role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(RoleViewModel newRoleVM)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = newRoleVM.RoleName;
                IdentityResult result=await roleManager.CreateAsync(role);
               if( result.Succeeded)
                {
                    return View();
                }
               else
                {
                    foreach( var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(newRoleVM);
        }
    }
}
