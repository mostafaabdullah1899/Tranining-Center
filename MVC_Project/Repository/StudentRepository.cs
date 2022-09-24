using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Repository
{
    //Services
    public class StudentRepository : IStudentRepository
    {
        ITIEntity context;// new ITIEntity();
        //inject as built in Service
        public StudentRepository(ITIEntity _context) //Di=>Dependency injection
        {
            context = _context;
        }
        //اى حد يتعامل مع السيرفز دى لازم يبعت اوبجكت من ITIEntity

        public Guid id { get; set; }
        public StudentRepository()
        {
            id=Guid.NewGuid();
            //اول ما الكونتينر يعمل اوبجكت من هذا الكلاس هيتكريت id 
        }

        //CRUD
        //Read
        public List<Student> getAll()
        {
            return context.Students.ToList();
        }
        public List<Student> getAllWithDepartment()
        {
            return context.Students.Include(s => s.Department).ToList();
        }
        public Student getById(int id)
        {
            return context.Students.FirstOrDefault(s => s.Id == id);
        }
        public Student getByIdWithDepartment(int id)
        {
            return context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }
        //Create
        public int Add(Student std)
        {
            context.Students.Add(std);
            int row = context.SaveChanges();
            return row;

        }
        //Update
        public int Update(int id, Student newStd)
        {
            Student oldStd = context.Students.FirstOrDefault(s => s.Id == id);
            oldStd.Name = newStd.Name;
            oldStd.Address = newStd.Address;
            oldStd.Age = newStd.Age;
            oldStd.Image = newStd.Image;
            oldStd.Dept_Id = newStd.Dept_Id;
            return context.SaveChanges();
        }
        //Delete
        public int Delete(int id)
        {
            Student std = context.Students.FirstOrDefault(s => s.Id == id);
            context.Students.Remove(std); //Remove of Memory only
            return context.SaveChanges();

        }
    }
}
