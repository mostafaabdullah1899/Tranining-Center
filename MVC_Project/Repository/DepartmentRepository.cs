using MVC_Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        ITIEntity context; //new ITIEntity();

        public DepartmentRepository(ITIEntity _context)
        {
            context = _context;
        }
        public List<Department> getAll()
        {
            return context.Departments.ToList();
        }
        public Department getById(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }
        public int Add(Department dept)
        {
            context.Departments.Add(dept);
            return context.SaveChanges();
        }
        public int Update(int id, Department newDept)
        {
            Department oldDept = new Department();
            oldDept.Name = newDept.Name;
            oldDept.ManagerName = newDept.ManagerName;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            Department dept = context.Departments.FirstOrDefault(d => d.Id == id);
            context.Departments.Remove(dept);
            return context.SaveChanges();
        }
    }
}
