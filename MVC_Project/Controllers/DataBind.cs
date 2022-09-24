using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Collections.Generic;

namespace MVC_Project.Controllers
{
    public class DataBind : Controller
    {     //Binding Primitive Type and array
        //DataBind/BindPremitive?name=mostafa&age=23&id=1 =>All Data in Query String
        //DataBind/BindPremitive/1?name=mostafa&age=23&id=1 =>All Data in Query String and id in Route Value
        //DataBind/BindPremitive?name=mostafa&age=23&id=1&degrees=50&degrees=60&degrees=70 =>All Data in Query String
        public IActionResult BindPremitive( int age , string name ,int id, int []degrees)
        {
            return Content("OK");


        }
        //Binding Data Collection
        //DataBind/BindDictionary?name=mostafa&phones[ali]=50&phones[sayed]=>All Data in Query String
        public IActionResult BindDictionary(string name, Dictionary<string, string> phones)
        {
            return Content("OK");
        }
        //DataBind/BindList?name=mostafa&phones=50&phones=>All Data in Query String no index
        public IActionResult BindList( List<string> phones)
        {
            return Content("OK");
        }

        //ComplexBind ModelBind CustumBind

        public IActionResult BindComplex( string name , Department dept ) //binding on primitive in department name id MangerName
        {
            return Content("OK");
        }
    }  

}
