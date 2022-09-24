using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC_Project.Models
{
    public class UniqueCourseAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return null;
            }
            else
            {
                
               string newCourseName= value.ToString();
                ITIEntity context= new ITIEntity();
               var oldCourseName= context.Courses.FirstOrDefault(c => c.Name == newCourseName);
                //if(oldCourseName != null &&oldCourseName.Id!= validationContext.Id)
                {
                   return new ValidationResult("Name must be Unique");
                }
                return ValidationResult.Success;
            }
            
        }
    }
}
