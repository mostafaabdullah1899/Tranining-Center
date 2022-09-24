using Microsoft.AspNetCore.Mvc;
using System;

namespace MVC_Project.Controllers
{
    public class FirstController : Controller
    {

        public ContentResult getString()
        {
            ContentResult result = new ContentResult();
            result.Content = "Welcome Mostafa";
            // return result;
            return Content("We");
        }
        public ViewResult getView()
        {
            ViewResult result = new ViewResult();
            result.ViewName = "MyView";
            return result;
        }

        public IActionResult getJson()
        {
            ////JsonResult json = new JsonResult(new { ID = 22, Name = "Mostafa" });
            ////return json;
            ///
            return Json(new { ID = 22, Name = "Mostafa1" });
        }
        // IActionResult  Parent Class to All Data Type
        public IActionResult getMax()
        {
            if(DateTime.Now.Day==6)
            {
                //ContentResult content = new ContentResult();
                //content.Content = "No Page";
                //return content;

                return Content("No Page");
            }
            else
            {
                //ViewResult view = new ViewResult();
                //view.ViewName = "MyView";
                //return view;

                return View("MyView");
            }
        }
        // view using more than controller put in Shared
        //view in share accepable in any controller
    }
}
