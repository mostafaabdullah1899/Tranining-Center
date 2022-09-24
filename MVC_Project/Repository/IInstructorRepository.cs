using MVC_Project.Models;
using System.Collections.Generic;

namespace MVC_Project.Repository
{
    public interface IInstructorRepository
    {
        int Add(Instructor inst);
        int Delete(int id);
        List<Instructor> getAll();
        List<Instructor> getAllWithDept();
        Instructor getById(int id);
        int Update(int id, Instructor newInst);
    }
}