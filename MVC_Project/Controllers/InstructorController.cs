using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System.Linq;
using MVC_Project.Repository;

namespace MVC_Project.Controllers
{
    public class InstructorController : Controller
    {
        //ITIEntity contxt = new ITIEntity();

        IInstructorRepository instRepo; //D inversion
        IDepartmentRepository deptRepo;
        public InstructorController(IInstructorRepository _instRepo , IDepartmentRepository _deptRepo) //D injection
        {
            instRepo = _instRepo;
            deptRepo = _deptRepo;
        }

        public IActionResult Index()
        {
            return View(instRepo.getAllWithDept());
        }
        public IActionResult Details(int id)
        {
           
            return View(instRepo.getById(id));
        }
        
        public IActionResult Add()
        {
            ViewData["dept"] = deptRepo.getAll();
            return View(new Instructor());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAdd(int id, Instructor inst)
        {
            ViewData["dept"] = deptRepo.getAll();
            if( ModelState.IsValid)
            {
                instRepo.Add(inst);
                return RedirectToAction("Index");
            }
            return View("Add",inst);
           
        }
        public IActionResult Edit(int id)
        {
            ViewData["dept"] = deptRepo.getAll();

            Instructor inst = instRepo.getById(id);
            return View(inst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(int id, Instructor newInst)
        {
           
            if(newInst.Name !=null)
            {
                instRepo.Update(id, newInst);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", new {id=newInst.Id});
            }

        }
    }
}
