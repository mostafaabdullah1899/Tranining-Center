using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Display(Name="Instructor Name")]
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }

        // [DataType(DataType.EmailAddress)]
        [Required]
        [Range(maximum: 30, minimum: 18)]
        public int Salary { get; set; }
       
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Course> Courses { get; set; }
    }
}
