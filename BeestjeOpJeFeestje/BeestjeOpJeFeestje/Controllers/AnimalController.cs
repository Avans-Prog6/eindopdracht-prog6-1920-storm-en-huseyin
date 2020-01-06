using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AnimalController : Controller
    {
        public AnimalController()
        {
            ViewData["Title"] = "Index";
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}