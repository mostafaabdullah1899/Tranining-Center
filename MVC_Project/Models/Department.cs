using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ManagerName { get; set; }
        public  List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }
        



    }
}
