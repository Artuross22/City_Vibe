using City_Vibe.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using City_Vibe.Domain.Models;
using City_Vibe.Contracts;

namespace City_Vibe.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService  categoryService;

        public CategoryController(ICategoryService categoryServ)
        {
            categoryService = categoryServ;
        }

        public async Task<IActionResult> ListOfCategories()
        {
            IEnumerable<Category> category = await categoryService.ListOfCategories();
            return View(category);
        }

        [HttpGet]
        public async Task<ActionResult> EditCategory(int? IdEdit)
        {
            if (IdEdit != null)
            {
                CategoryEditViewModel category = await categoryService.EditCategoryGet(IdEdit);

                if(category == null || category.Id == 0)
                {
                    return NotFound();
                }

                return View(category);

            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryEditViewModel category)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit category ");
                return View(category);
            }
            var request = categoryService.EditCategoryPost(category);
            return RedirectToAction("ListOfCategories", request);
        }


        [HttpGet]
        public IActionResult AddCategory()
        {       
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryAddViewModel categoryAddVM)
        {
            if (ModelState.IsValid)
            {             
                var response = categoryService.AddCategory(categoryAddVM);     
                return RedirectToAction("ListOfCategories", response.Success);
            }
            return View(categoryAddVM);
        }


       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var categoryDelete = await categoryService.DeleteCategory(id);

            if(categoryDelete == false)  return NotFound();


            return RedirectToAction(nameof(ListOfCategories));
           
        }
    }
}

