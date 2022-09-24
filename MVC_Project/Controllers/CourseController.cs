using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.Repository;
using System.Linq;

namespace MVC_Project.Controllers
{
    public class CourseController : Controller
    {
        ITIEntity context = new ITIEntity();
        ICourseRepository crsRepo;
        public  CourseController(ICourseRepository _crsRepo)
        {
           crsRepo= _crsRepo;
        }
        public IActionResult Index()
        {
            //Egar Loading include navigation property
            return View(context.Courses.Include(c=>c.Instructor).ToList());
        }
        public IActionResult Add()
        {
            ViewData["Inst"] = context.Courses.Include(c=>c.Instructor);
            return View();
        }

        public IActionResult SaveAdd(Course crs)
        {
            if (ModelState.IsValid && crs.InstructorId != 0)
            {
                crsRepo.Add(crs);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("InstructorId", "Select Instructor");
                ViewData["Inst"] = crsRepo.getAll();
                return View("Add", crs);
            }

        }
       
        public IActionResult CheckMinDegree(int Degree , int MinDegree)
        {
            if(MinDegree < Degree)
            {
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult Edit(int id)
        {
            ViewData["Inst"] = crsRepo.getAll();
            return View(crsRepo.getById(id));
        }

        public IActionResult SaveEdit(int id ,Course newCrs)
        {
           
            if(ModelState.IsValid && newCrs.InstructorId!=0)
            {
                crsRepo.Update(id, newCrs);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("InstructorId", "Select Instructor");
                ViewData["Inst"] = crsRepo.getAll();
                return View("Edit", newCrs);
            }
        }

        public IActionResult Details(int id)
        {
            return View(crsRepo.getById(id));
        }
        public IActionResult Delete(int id)
        {
            return View(crsRepo.getById(id));
        }
        public IActionResult ConfirmDelete(int id)
        {
            crsRepo.Delete(id);
            return RedirectToAction("Index");

        }
    }
}
