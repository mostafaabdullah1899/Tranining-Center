using MVC_Project.Models;
using System.Collections.Generic;

namespace MVC_Project.Repository
{
    public interface ICourseRepository
    {
        int Add(Course newCrs);
        int Delete(int id);
        List<Course> getAll();
        Course getById(int id);
        int Update(int id, Course newCrs);
    }
}