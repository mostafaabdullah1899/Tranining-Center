using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC_Project.Models
{
    //My Own Anntribute
    public class UniqueNameAttribute : ValidationAttribute
    {
        public string msg { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value== null)
            {
                return null;
            }
            else
            {
                string newName = value.ToString();
                ITIEntity context = new ITIEntity();
               var oldName= context.Students.FirstOrDefault(s=>s.Name==newName);
               // Student stdForm = (Student)ValidationContext.ObjectInstance;
                if (oldName!=null)
                {
                    return new ValidationResult ("Name must be Unique");
                }
            }
            return ValidationResult.Success; //validationContext return all object

        }

    }
}
