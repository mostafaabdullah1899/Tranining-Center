using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using MVC_Project.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Controllers
{
    public class DepartmentController : Controller
    {
        ITIEntity contxt = new ITIEntity();
        IDepartmentRepository departmentRepository;// = new DepartmentRepository();


        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }
        public IActionResult New()
        {
            return View(new Department()); //model=null new Department()
        }
        //[Acceptverbs[Get|Post]by default This Action handling get and post

        //<form method="post">
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew( Department dept)
        {
            if (dept.Name!=null)
            {
                departmentRepository.Add(dept);
                //contxt.Departments.Add(dept);
                //contxt.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
            return View("New",dept); //Model=Null
          //  return RedirectToAction("Index" ,"Department"); // return view model Index
           // return RedirectToAction("Details", new { id = dept.Id });
        }
        public IActionResult Index()
        {
           // List<Department> deptList = contxt.Departments.ToList();
            List<Department> deptList = departmentRepository.getAll();
            return View(deptList); //model=>deptList View=>Index

           // return View("Index",deptList) model => deptList View => Index
           // return View("Index") model => Null View => Index
             // return View() model => Null View => Index
        }

        public IActionResult Details(int id)
        {
            //default=Null
            // Department dept = contxt.Departments.First(d=>d.Id==id);
            Department dept = departmentRepository.getById(id);
            return View(dept);
        }
     
        public IActionResult Edit(int id)
        {

            // return View(contxt.Departments.FirstOrDefault(d => d.Id == id));
            return View(departmentRepository.getById(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken] //no external request
        public IActionResult SaveEdit(int id ,Department newdept)
        {
            //Department olddept = contxt.Departments.FirstOrDefault(d => d.Id == newdept.Id);
            //if(newdept.Name!=null)
            //{
            //    olddept.Name = newdept.Name;
            //    olddept.ManagerName=newdept.ManagerName;
            //    contxt.SaveChanges();
            if (newdept.Name != null) 
            { 
                departmentRepository.Update(id,newdept);
                return RedirectToAction("Index");
            }
            return View("Edit", newdept);
        }

        public IActionResult Delete(int id)
        {
            //Department dept = contxt.Departments.FirstOrDefault(d => d.Id == id);
            //contxt.Departments.Remove(dept);
            //contxt.SaveChanges();
            departmentRepository.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult GetStudents(int deptId)
        {
            return Json(contxt.Students.Where(s => s.Dept_Id == deptId).ToList());
            // Egar Execution All Filteration in DB
        }
        public IActionResult GetInstructors(int Dept)
        {
            return Json(contxt.Instructors.Where(i => i.DepartmentId == Dept).ToList());
        }

    }
}
