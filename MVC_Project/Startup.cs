using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MVC_Project.Models;
using MVC_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
         {
            // Add framework services.
          

            //Services=>service Provider
            services.AddSession(SessionOptions=> //Build in Service
            {
                SessionOptions.IdleTimeout = TimeSpan.FromMinutes(20);
            }); //handling session properity
            services.AddControllersWithViews();//.AddSessionStateTempDataProvider;
              //Buildin Services
            services.AddDbContext<ITIEntity>(options =>options.UseSqlServer(Configuration.GetConnectionString("CS")));





            //GetConnectionString=>Read of ConnectionStrings Key in appsetting.json
            // .Configration.GetConnectionString("CS");
            // services.AddDbContext<ITIEntity>(option => option.UseSqlServer("Data Source=DESKTOP-EIFLNP4;Initial Catalog=Mostafa1899;Integrated Security=True"));
            //Security Module
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.Password.RequireDigit=true).AddEntityFrameworkStores<ITIEntity>();
            //AddIdentity=>Register UM , RM , SignInM
            //AddEntityFrameworkStores<ITIEntity>();=>UserStore,RoleStore =>inject ITIEntity

            //Register my Custom Services in IOC Container Service Provider For Resolve Services
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IInstructorRepository,InstructorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            //any Constructor inject  IStudentRepository Create For it object from StudentRepository

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region  //Custum MiddleWare inline pipe line
            //Next To MiddleWare

            //app.Use(async (httpContext,Next) => 
            //{
            //    await httpContext.Response.WriteAsync("MiddeleWare1\n");
            //    await Next.Invoke();
            //    await httpContext.Response.WriteAsync("MiddleWare1_1\n");

            //});


            //app.Use(async (httpConext, Next) =>
            //{
            //    await httpConext.Response.WriteAsync("MiddleWare2\n");
            //    await Next.Invoke();
            //    await httpConext.Response.WriteAsync(" MiddleWare2_2\n");
            //});
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Terminate\n");

            //}
            //    );
            #endregion

            //Pipe line set of middlewares
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //if  development Status using middleware DeveloperExceptionPage
                //The First MiddleWare and the End
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //if deployment status using   middleware ExceptionHandler
            }
            app.UseStaticFiles();
            // Read of /handling requests  wwwRoot
            // is first short circuit

            app.UseRouting();
            //matching URL Controler/Action
            //تفهم الفلتر ازاى يتشك على الكوكى اثناء الريكوست
            app.UseAuthentication(); //Autorize Filter Check on Cookie
            app.UseAuthorization();
            app.UseSession(); //handling session properity

           
            app.UseEndpoints(endpoints =>
            {// Route Confinshaion Using in MVC
                endpoints.MapControllerRoute(
                 name: "default",
                 //pattern: "ITI/", //http://localhost:50990/ITI?age=22&name=asd as Query string
                 pattern: "ITI/{age:int:max(50)}/{name:alpha}", //route segmant  name , age Mondatory with condation
                 defaults: new { controller = "Student", action = "TestRoute" });
                endpoints.MapControllerRoute( //Define Route and Mapping create object of Controller
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");
                //  /sigmant/ { هتتشال ويتحط مكانها} / هتظهر فى الurl/
                //pattern: "{controller=Home}/{action=Index}/{id?}/{name}");
                // pattern: "/{action=Index}/{controller=Home}/{id?}");
                // pattern: "ITI/{action=Index}/{controller=Home}/{id?}");
                //ITI=>Litteral Required in URL
                //pattern: "{controller=Department}/{action=Index}/{id?}");
                //default department
                  // pattern: "{controller=Home}/{action=Index}/{id}");
                  //{id?} ?=> Null optianal
        });
        }
    }
    }
