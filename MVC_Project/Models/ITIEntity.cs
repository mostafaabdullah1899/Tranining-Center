using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MVC_Project.Models
{
    //InCase  IdentityDbContext DBSet<IdentityUser>
    //InCase  IdentityDbContext<ApplicationUSer> DBSet<ApplicationUser>
    //add-migration search on class inherit DbContext
    public class ITIEntity : IdentityDbContext<ApplicationUser>
    {
        //Constructor take option of OnConfiguring
        public ITIEntity():base()
        {

        }
//Construtor For  services.AddDbContext<ITIEntity>(option => option.UseSqlServer());
        public ITIEntity (DbContextOptions<ITIEntity>options) : base()
        {

        }
        //Send Options (Connection String) parameter of Service or onConfiguring For base() 

        //this Constructor For Service
        //public ITIEntity(DbContextOptions options) : base()
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EIFLNP4;Initial Catalog=Mostafa1899;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set;}
    }
}
