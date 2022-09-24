using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Collections.Generic;

namespace MVC_Project.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            //ask model 
           SampleData  sampleData = new SampleData();
           List<Product> producrList= sampleData.GetAll();
            // send to view view name and model object
            return View("DisplayAllProducts",producrList);
        }
        //product/details/1
        //product/details?id=1
        public IActionResult Details(int id)
        
        
        
        
        
        
        
        
        {
            SampleData sampleData= new SampleData();
           Product product= sampleData.getById( id);
            return View("Details",product);
        }
    }
}
