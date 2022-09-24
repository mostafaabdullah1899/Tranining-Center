using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MVC_Project.Controllers
{
    public class PassDataController : Controller
    {
        public IActionResult setCookie()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddDays(1);
            //server set data in response to cookie
            Response.Cookies.Append("name", "Mostafa Abdullah"); //Seesion Cookie expire if session end
            Response.Cookies.Append("Age", "22", cookieOptions); //persisisiten cookiew expire after 1 day
            return Content("Cookies Saved");
        }

        public IActionResult getCookie()
        {
            //Server Request data from coookie

           string name= Request.Cookies["name"];
          string age=  Request.Cookies["age"];
            return Content($"Name={name} Age={age}");


        }


        public IActionResult setSession()
        {
            HttpContext.Session.SetString("name", "mostafa");
            HttpContext.Session.SetInt32("age", 23);
            return Content("Session Saved");
        }

        public IActionResult getSession()
        {
            string name = HttpContext.Session.GetString("name");
            int age = (int)HttpContext.Session.GetInt32("age");
            return Content($"Age={age} Name={name}");

        }





        public IActionResult First()
        {
            TempData["Msg"] = " Welcome For TempData";

            return Content("Data Saved");
        }
        public IActionResult Second()
        {
            string msg;
            if (TempData.ContainsKey("Msg") )
            {
                // msg = TempData["Msg"].ToString();
                msg =TempData.Peek("Msg").ToString();
                //peek reed of tempData and store in it and return
            }
            else
            {
                msg = "TempData Empry";
            }
            return Content("Second"+msg);
        }
        public IActionResult Third()
        {
            string msg = TempData["Msg"].ToString(); //Normal Reed
          //  TempData.Keep("Msg");
            //keep reed of tempData and store in it and return void
            return Content("Third"+msg);
        }
    }
}
