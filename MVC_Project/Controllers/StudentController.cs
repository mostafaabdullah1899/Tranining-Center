using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Collections.Generic;
using System.Linq;
using MVC_Project.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using MVC_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MVC_Project.Controllers
{
    [Authorize] //check on cookie using middleWare app.UseAuthentication
    public class StudentController : Controller
    {
        ITIEntity context = new ITIEntity();
        IStudentRepository studentRepository; //Dependency injection Principle General
        //inject as Custom Service
        IDepartmentRepository departmentRepository ;
        //DIP=> Dependency inversion Principle ioc inverion of controller create interface
        //Constructor injection of injection types
        public StudentController(IStudentRepository stdRepo , IDepartmentRepository deptRepo)
        {
            //DI=>inject interface in constructor
            //DI=>Dependency Injecton Created By Controller -Constructor injection 
            //IOC Container Create object of any Class implement interface
            studentRepository = stdRepo;
            departmentRepository= deptRepo;

        }
        //Include(s=>s.Department.Name)
        [HttpGet] //<a> Link
        public IActionResult Action1()
        {
            return Content("HTTP Get <a> Link");
        }
        [HttpPost]
        public IActionResult Action1(int id ,string salary)
        {
            return Content("HTTP Post <a> ");
        }
        public IActionResult Add()
        {
           // ViewData["dept"] = context.Departments.ToList();
            ViewData["dept"]=departmentRepository.getAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //internal call of same domain request Check on Token name &value
        public IActionResult SaveAdd( Student std)
        { if (ModelState.IsValid && std.Dept_Id != 0)
            { //Custom Validation

                //context.Students.Add(std);
                //context.SaveChanges();
                studentRepository.Add(std);
                return RedirectToAction("Index");
                
                //catch(Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //}

            }
            // Error Message send in view
            else
            { //Etra Error
                ModelState.AddModelError("Dept_Id", "Please Select Department");
                //Error Display in Span and div(All) span because Dept_Id is key and properity in model
              // ModelState.AddModelError("Dept", "Please Select Department");
                //Error Display in div(All) Only because Dept is not key and properity in model

               // ViewData["dept"] = context.Departments.ToList();
                ViewData["dept"] = departmentRepository.getAll();
                return View("Add", std);
            }
            
        }
        public IActionResult Index()
        {
            string name = User.Identity.Name; //return value of type Name
            // User.Claims.FirstOrDefault(c => c.Type == "URL");
            //ClaimTypes.NameIdentifier return  URL ID=>type
           string id =  User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;  //return value of type ID
           //string address = User.Identity.Address;

            ViewData["ServiceId"] = studentRepository.id;
            return View(studentRepository.getAllWithDepartment());
            //return PartialView(context.Students.Include(s => s.Department).ToList());
            //1-return Index view without View Start
        }
        [AllowAnonymous] //no check on cookie
        public IActionResult Edit(int id)
        {
            ViewData["dept"] = departmentRepository.getAll();
            // return View(context.Students.FirstOrDefault(s=>s.Id==id));
            return View(studentRepository.getById(id));
            //get all department

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit([FromRoute]int id, Student newStd)
        {
            //Student oldStudent=  context.Students.FirstOrDefault(s => s.Id == id);
            //Student oldStudent = studentRepository.getById(id);
            if (ModelState.IsValid)
            {
                //   oldStudent.Id = newStd.Id; Not Editable validation client side
                //oldStudent.Name=newStd.Name;
                //oldStudent.Address = newStd.Address;
                //oldStudent.Age = newStd.Age;
                //oldStudent.Image = newStd.Image;
                //oldStudent.Dept_Id = newStd.Dept_Id;
                //context.SaveChanges();
                studentRepository.Update(id,newStd);
                return RedirectToAction("Index", "Student");
            }
         // return RedirectToAction("Edit", new {id=newStd.Id});
        ViewData["dept"] = context.Departments.ToList();
         return View("Edit", newStd); //send model only without ViewData 
        }
        // all request have object of  StudentController has ViewData
        //ViewData Dictionar<key string , value object>
       
        public IActionResult Details( int id)
        {
            List<string> branches = new List<string>();
            branches.Add("Assuit");
            branches.Add("Alex");
            branches.Add("Smart");
            ViewData["branch"] = branches;
            ViewData["msg"] = "Welcome iti";
            ViewBag.temp = 30;
            //Student student=context.Students.FirstOrDefault(s => s.Id == id);
            Student student = studentRepository.getById(id);


            return View(student);
        }

        public IActionResult DetailsVM()
        {
            List<string> branches = new List<string>();
            branches.Add("Assuit");
            branches.Add("Monofia");
            branches.Add("Alex");
            var student=context.Students.FirstOrDefault();
           // var student=studentRepository.getById();

            StudentWithBranchListViewModel studentVM = new StudentWithBranchListViewModel();
            studentVM.branches = branches;
            studentVM.StdName = student.Name;
            studentVM.StdAddress = student.Address;
            studentVM.StdAge = student.Age;
            studentVM.msg = "Welcome ViewModel";
            studentVM.temp = -2;
            if(studentVM.temp > 0)
            {
                studentVM.color = "red";
            }
            else
            {
                studentVM.color = "blue";
            }
            return View(studentVM);
        }
      
        //Remote Attribute Using Ajax Call client Side with unobtrusive
        //InCase ModelBinder Bining Name from Jquery
        //String Name mapping Property Name Read Value of Input
        //Execute Method when Blur
        public IActionResult CheckName( String Name ,String Address)
        {
            if(Address.Contains("Alex") &&Name.Contains("ITI") )
            {
                return Json(true);
            }
            else
            {
                return Json(false);
                //Display Error message
            }
        }

        public IActionResult NewDetails(int id)
        {
            return View(studentRepository.getByIdWithDepartment(id));
        }
        public IActionResult Delete(int id)
        {
            // var std = context.Students.Include(s=>s.Department).FirstOrDefault(s => s.Id == id);
            var std = studentRepository.getByIdWithDepartment(id);
            return View(std);
        }

        public IActionResult ConfirmDelete(int id)
        {
            //var std= context.Students.FirstOrDefault(s => s.Id == id);
            //  context.Students.Remove(std);
            //  context.SaveChanges();
            studentRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult TestPartialView(int id)
        { 
            //var std = context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
            var std= studentRepository.getByIdWithDepartment(id);
            return PartialView("NewDetails", std); // 1- Student Card Only return NewDetails without Layout
            //return View("_StudentCard", std); // 1-ViewStart 2- Render Body as Student Card return StudentCard with Layout
          // return PartialView("_StudentCard", std);
        }


        public IActionResult TestAjax()
        {
           // List<Student> stdList = context.Students.ToList();
            List<Student> stdList = studentRepository.getAll();
            return View(stdList);
        }
        [Route("ITI/{age}/{name}")] 
        // Using in Api
        // Route Attribute مش هينفع انادى الاكشن غير بالطريقة دى بس 
        // مش هيماتش الروت فى حته تانى
        public IActionResult TestRoute(int age, string name)
        {
            return Content($" Ok ----> Name:{name} -  Age:{age}");
        }
        //public IActionResult TestRoute()
        //{
        //    return Content($" Ok ");
        //}

    }
}
