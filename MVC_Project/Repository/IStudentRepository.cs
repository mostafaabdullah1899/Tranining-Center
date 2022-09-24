using MVC_Project.Models;
using System;
using System.Collections.Generic;

namespace MVC_Project.Repository
{
    public interface IStudentRepository
    {
        public Guid id { set; get; }
        int Add(Student std);
        int Delete(int id);
        List<Student> getAll();
        List<Student> getAllWithDepartment();
        Student getById(int id);
        Student getByIdWithDepartment(int id);
        int Update(int id, Student newStd);
    }
}