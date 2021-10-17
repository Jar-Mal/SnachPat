using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnachPat.Models;
using SnachPat.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SnachPat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PhotoService _photoService;

        public HomeController(ILogger<HomeController> logger, PhotoService photoService)
        {
            _logger = logger;
            _photoService = photoService;
        }

        public IActionResult Index()
        {
            return View("~/Views/Home/Chat.cshtml");
        }
        public IActionResult PostPhoto([FromForm] IFormFile photo)
        {
            _photoService.AddPhoto(photo);
            return RedirectToAction("GetPhoto", "Home");

        }
        public IActionResult PostPhotoToChat([FromForm] IFormFile photo)
        {
            _photoService.AddPhoto(photo);
            TempData["path"] = _photoService.GetNewestPhotoPath();
            return RedirectToAction("Chat", "Home");
        }

        [HttpGet("GetPhoto")]
        public IActionResult GetPhoto()
        {
            TempData["path"] = _photoService.GetNewestPhotoPath();
            return View();
        }

        [HttpGet("GetPath")]
        public string GetPathOnClick()
        {
            var path = _photoService.GetNewestPhotoPath();
            return path is null ? "" : path;
        }
        [HttpGet("Chat")]
        public ActionResult Chat()
        {
            return View();
        }
    }
}
