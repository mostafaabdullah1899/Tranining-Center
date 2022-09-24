using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        ITIEntity context; //dependency inversion
        public InstructorRepository(ITIEntity _context) //dependency injection
        {
            context = _context;
        }
        public List<Instructor> getAll()
        {
            return context.Instructors.ToList();
        }
        public List<Instructor> getAllWithDept()
        {
            return context.Instructors.Include(i => i.Department).ToList();
        }
        public Instructor getById(int id)
        {
            return context.Instructors.FirstOrDefault(i => i.Id == id);
        }
        public int Add(Instructor inst)
        {
            context.Instructors.Add(inst);
            return context.SaveChanges();
        }
        public int Update(int id, Instructor newInst)
        {
            Instructor oldInst = getById(newInst.Id);
            oldInst.Name = newInst.Name;
            oldInst.Salary = newInst.Salary;
            oldInst.DepartmentId = newInst.DepartmentId;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            context.Instructors.Remove(getById(id));
            return context.SaveChanges();
        }
    }
}
