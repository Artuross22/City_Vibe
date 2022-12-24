using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace City_Vibe.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }

        public async Task<IActionResult> ListOfCategories()
        {
            var category = categoryRepository.GetAll();
            return View(category);
        }

        [HttpGet]
        public IActionResult AddCategories()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategories(CategoriesAddViewModel categoryAddVM)
        {

            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = categoryAddVM.Name
                };

                categoryRepository.Add(category);
                return RedirectToAction("TestResult");
            }
            return View(categoryAddVM);
        }


    }


}

