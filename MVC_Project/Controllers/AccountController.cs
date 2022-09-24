using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using MVC_Project.ViewModel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager; //Dependency inversion
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager) //Dependency injection
        {
            userManager = _userManager; //Service Provider Create object of UserManager<ApplicationUser>
            signInManager = _signInManager;
        } 
        [HttpGet]//<a>
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if(ModelState.IsValid)
            { //Create Account
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName=newUserVM.UserName;
                userModel.PasswordHash = newUserVM.Password;
                userModel.Address = newUserVM.Address;
                IdentityResult result= await userManager.CreateAsync(userModel, newUserVM.Password); //userManager=>userStor=>Context=> retun Identity
                //CreateAsync(userModel, newUserVM.Password) =>OverLoad For Adding Validation on Password
                if (result.Succeeded) //Record Saved
                {
                    //signInManager=>Create Cookie
                    //isPersistent: false=> Cookie expire after 20Mint
                    await signInManager.SignInAsync(userModel, isPersistent: false); //login
                    return RedirectToAction("Index", "Student");
                }
                else //Record not Saved By userManager
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    
                }
            }

            return View(newUserVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
           if(ModelState.IsValid)
            {
                //Search by UserName
                ApplicationUser userModel=  await userManager.FindByNameAsync(userVM.UserName);
                if(userModel!=null)
                {
                    bool found= await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if(found )
                    {
                        //Create Cookie set of Claims Cotains ID , Name Only
                        // await signInManager.SignInAsync(userModel, userVM.RememberMe);

                        //Create Cookie set Claims Cotains ID , Name ,Address

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Address", userModel.Address));
                        await signInManager.SignInWithClaimsAsync(userModel, userVM.RememberMe, claims);

                        return RedirectToAction("Index", "Student");
                        //if rememberme checked=>presistent cookie
                        //else seesion cookie
                    }
                }
                
                ModelState.AddModelError("", "UserName and Password invalid");   
            }

           return View(userVM);
  
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync(); //Dispose Cookie
            return RedirectToAction("Login");
        }
        //Create Admin 
        // Assign Roles For Users
        //Create Account and Assign for him Roles
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task< IActionResult> AddAdmin(RegisterUserViewModel newUserVM)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUserVM.UserName;
                userModel.PasswordHash = newUserVM.Password;
                userModel.Address = newUserVM.Address;
                IdentityResult result =await userManager.CreateAsync(userModel,newUserVM.Password);//Add validation For Password
                if(result.Succeeded)//Row added
                {
                    //Assign Role to User
                  IdentityResult result2=  await userManager.AddToRoleAsync(userModel, "Admin"); //wait adding role for cookie
                    if(result2.Succeeded)
                    {
                        //Create Cookie
                        await signInManager.SignInAsync(userModel, false);
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        foreach( var item in result2.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                   
                else
                {
                    foreach(var item in  result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }
            }
            return View(newUserVM);
        }
    }
}
