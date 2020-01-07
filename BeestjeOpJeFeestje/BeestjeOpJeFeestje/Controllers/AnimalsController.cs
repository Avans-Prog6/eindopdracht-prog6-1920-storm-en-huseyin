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
            List<Animal> animals = _repository.GetAll();
            return View(animals);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int ID)
        {
            Animal animal = _repository.Get(ID);
            return View(animal);
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}