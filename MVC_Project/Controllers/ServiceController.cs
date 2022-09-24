using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Repository;
using System;
using System.IO;

namespace MVC_Project.Controllers
{
    public class ServiceController : Controller
    {
        IStudentRepository studentRepository;
        IWebHostEnvironment webHostEnvironment;
        public ServiceController(IStudentRepository _studentRepository , IWebHostEnvironment _webHostEnvironment)
        {
            studentRepository = _studentRepository;
            //شايله اوبجكت من السيرفز اللى اسمها Student Repository
            this.webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewData["ServiceId"]=studentRepository.id;
            return View();
        }
        public IActionResult uploadImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult uploadImage(IFormFile Image)
        {
            //path wwwroot
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
            //add key For image
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
                fileStream.Close();
            }

            return View();
        }
    }
}
