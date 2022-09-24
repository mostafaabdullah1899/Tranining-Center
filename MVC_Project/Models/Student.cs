

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Student
    {
        public int Id { get; set; }
        // [DataType(DataType.EmailAddress)]
        
        [Display(Name="Student Name")]
        [Required]
        [MaxLength(20,ErrorMessage =" Name must be less than 21 letter")]
        [MinLength(3, ErrorMessage = " Name must be greater than 2 letter")]
        //Over ride on error message
        //[RegularExpression("[a-zA-Z]{3,20}")]
        //[UniqueName]
        [Remote(action:"CheckName",controller:"Student",
            AdditionalFields ="Address"
            ,ErrorMessage="Name Must contain ITI")]
        //By Default Send Property Name For Action CheckName
        public string Name { get; set; }
        
        //[UniqueName(msg ="")]
        [Display(Name ="Student Address")]
        [Required]
        [RegularExpression(@"(Assuit|Alex|Cairo|Giza)" ,ErrorMessage ="Address must be Alex or Assuit")]
      //  [RegularExpression(@"(Assuit|Alex|Cairo|Giza)")]
        public string Address { get; set; }
        [Display(Name="Student Age")]
        [Required]
       // [Range(18,30)]
        [Range(maximum:30,minimum:18,ErrorMessage ="Age Must be between 18:30")]
        public int Age { get; set; }
        [Required]
        [Display(Name="Student Image")]
        //[RegularExpression(@"\w.(jpg|png)")]
        //downloadpng
        [RegularExpression(@"\w+\.(jpg|png)" ,ErrorMessage ="Image Must be jpg or png")]
        // download.png \. Dot Operator not char
        //\w Characters only
        public string Image { get; set; }
        [ForeignKey("Department")]
        [Display(Name="Department")]
        [Required]
        public int Dept_Id { get; set; }

        //Navigation Properties

        public virtual Department Department{ get; set; }
        //virual when create object of student doesnot create object of department using object in dbcontext
        //virtual assign lazy loading when Dbcontext call object doesnot call navigation properties
        
        
    }
}
