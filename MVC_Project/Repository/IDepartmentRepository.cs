using MVC_Project.Models;
using System.Collections.Generic;

namespace MVC_Project.Repository
{
    public interface IDepartmentRepository
    {
        int Add(Department dept);
        int Delete(int id);
        List<Department> getAll();
        Department getById(int id);
        int Update(int id, Department newDept);
    }
}