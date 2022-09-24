using MVC_Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ITIEntity context;

        public CourseRepository(ITIEntity _context)
        {
            context = _context;
        }
        public List<Course> getAll()
        {
            return context.Courses.ToList();
        }
        public Course getById(int id)
        {
            return context.Courses.FirstOrDefault(c => c.Id == id);
        }
        public int Add(Course newCrs)
        {
            context.Courses.Add(newCrs);
            return context.SaveChanges();
        }
        public int Update(int id, Course newCrs)
        {
            Course oldCrs = getById(id);
            oldCrs.Name = newCrs.Name;
            oldCrs.Degree = newCrs.Degree;
            oldCrs.MinDegree = newCrs.MinDegree;
            oldCrs.InstructorId = newCrs.InstructorId;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            var course = getById(id);
            context.Courses.Remove(course);
            return context.SaveChanges();
        }
    }
}
