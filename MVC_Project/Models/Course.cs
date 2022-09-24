using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVC_Project.Models
    
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [UniqueCourse]
        public string Name { get; set; }

        [Required]
        [Range(50,100)]
        public int Degree { get; set; }
        [Required]
       // [Range(25, 50)]
        [Remote(action: "CheckMinDegree", controller: "Course",
            AdditionalFields = "Degree"
           , ErrorMessage = "MinDegree mustbe lessThan Degree")]
        public int MinDegree { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
}


    }
