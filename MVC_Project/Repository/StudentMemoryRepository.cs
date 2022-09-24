using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Repository
{
    public class StudentMemoryRepository : IStudentRepository
    {
        List<Student> std = new List<Student>();
         public StudentMemoryRepository()
         {
            std.Add(new Student() { Id = 1, Address = "Assuit", Age = 22, Image = "1.png", Dept_Id = 1 });
            std.Add(new Student() { Id = 2, Address = "Assuit", Age = 22, Image = "1.png", Dept_Id = 1 });
        }

        public Guid id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Add(Student std)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Student> getAll()
        {
            return std;
        }
        public Student getById(int id)
        {
            return std.FirstOrDefault(s => s.Id == id);
        }

        public int Update(int id, Student newStd)
        {
            throw new System.NotImplementedException();
        }

        List<Student> IStudentRepository.getAllWithDepartment()
        {
            throw new System.NotImplementedException();
        }

        Student IStudentRepository.getByIdWithDepartment(int id)
        {
            throw new System.NotImplementedException();
        }

        //Create


    }
}
