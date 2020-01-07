using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IRepository<Animal> _repository;

        public AnimalsController(IRepository<Animal> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            Animal testAnimal = _repository.Get();
            return View();
        }
    }
}