
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Context;
using ToDoApp.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ToDoApp.Controllers
{



    public class CategoryController : Controller
    {

        /*To add toastify */
        private readonly INotyfService _toastNotification;
        public CategoryController( INotyfService toastNotification)
        {
            _toastNotification = toastNotification;
        }



        TodoContext TodoContext = new TodoContext();
        public IActionResult Index()
        {

            int UserId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var Category = TodoContext.Categories.Where(cat => cat.UserId == UserId).ToList();
            return View(Category);
        }
        /*Add new Category*/
        public IActionResult AddCategory()
        {
            return View();
        }
        public IActionResult saveCategory(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                newCategory.UserId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
                TodoContext.Categories.Add(newCategory);
                TodoContext.SaveChanges();
                _toastNotification.Success("New Category Has Been Saved.");
                return RedirectToAction("Index");
            }
            _toastNotification.Error("Can't Save New Category");
            return RedirectToAction("Index");
        }

        /*Update Category*/

        public IActionResult UpdateCategory(int Id)
        {
            var Category = TodoContext.Categories.FirstOrDefault(c => c.Id == Id);
            return View("AddCategory", Category);
        }
        public IActionResult SaveUpdateCategory(int Id, Category updateCategory)
        {
            var oldCategory = TodoContext.Categories.FirstOrDefault(cat => cat.Id == Id);
            if (oldCategory == null)
            {
                _toastNotification.Error("Can't Update Category");
                return View("AddCategory", oldCategory);
            }
            else if (ModelState.IsValid)
            {

                _toastNotification.Success("Category Updated");
                oldCategory.Name = updateCategory.Name;
                TodoContext.SaveChanges();
                return RedirectToAction("Index");
            }
            _toastNotification.Error("Can't Update Category");
            return View("AddCategory", oldCategory);

        }
      
        public IActionResult DeleteCategory(int Id)
        {
            var category = TodoContext.Categories.FirstOrDefault(c => c.Id == Id);
            if (category == null)
            {
                _toastNotification.Error("Can't Remove Category");
                return RedirectToAction("Index");
            }
            var isFound = TodoContext.Todos.FirstOrDefault(c => c.CategoryId == Id);
            if(isFound == null)
            {
                TodoContext.Categories.Remove(category);
                TodoContext.SaveChanges();

                return RedirectToAction("Index");
            }
            _toastNotification.Error("Categroy cannot Removed, it is Assign to a task");
            return RedirectToAction("Index");
        }
        
    }
}
