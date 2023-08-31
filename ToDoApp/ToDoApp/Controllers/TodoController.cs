using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TodoController : Controller
    {

        private readonly INotyfService _toastNotification;
        public TodoController(INotyfService toastNotification)
        {
            _toastNotification = toastNotification;
        }


        TodoContext TodoContext = new TodoContext();
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("TodoTask");
            }
            return View();
        }

       public IActionResult TodoTask()
        {
            var userPrincipal = HttpContext.User;
            var sidClaim = userPrincipal.FindFirst(ClaimTypes.Sid);
            var CurrUserId = int.Parse(sidClaim.Value);
            var Tasks = TodoContext.Todos.Where(user => user.UserId == CurrUserId).Include(Task => Task.Category).ToList();
            return View(Tasks);
        }
        public IActionResult createTask()
        {
            int UserId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
/*            var Category = TodoContext.Categories.Where(cat => cat.UserId == UserId).ToList();
*/            ViewData["Catogry"] = TodoContext.Categories.Where(cat => cat.UserId == UserId).ToList();
            return View();
        }


        public IActionResult SaveTask(Todo newTask)
        {
            newTask.CreatedDate = DateTime.Now;
            newTask.status = "NO";


            // Find the claim with the specified ClaimType
            var userPrincipal = HttpContext.User;
            var sidClaim = userPrincipal.FindFirst(ClaimTypes.Sid);
            newTask.UserId = int.Parse(sidClaim.Value);

            TodoContext.Todos.Add(newTask);
            TodoContext.SaveChanges();
            _toastNotification.Success("Task Created Successfully");
            return RedirectToAction("TodoTask");
        }
       
        public IActionResult DeleteTask(int Id)
        {
/*            Console.WriteLine*//**//*(Id);
*/            var TaskRemove = TodoContext.Todos.FirstOrDefault(todo => todo.Id == Id);
           
            if (TaskRemove == null)
            {
                return View("CannotRemove");
            }
            TodoContext.Todos.Remove(TaskRemove);
            TodoContext.SaveChanges();
            _toastNotification.Success("task Deleted Successfully");
            return RedirectToAction("Index");
        }

        /*Update Operation*/
        public IActionResult UpdateTask(int Id)
        {
            var task = TodoContext.Todos.FirstOrDefault(todo => todo.Id == Id);
            if (task == null)
            {
                return View("CannotRemove");
            }
            ViewData["Catogry"] = TodoContext.Categories.ToList();
            return View("createTask", task);

        }
        public IActionResult saveUpdateTask(int Id, Todo UpdateTodo)
        {
            var task = TodoContext.Todos.FirstOrDefault(todo => todo.Id == Id);
            if (task == null)
            {
                return View("CannotRemove");
            }

            task.Name = UpdateTodo.Name;
            task.Description = UpdateTodo.Description;
            task.CategoryId = UpdateTodo.CategoryId;
            TodoContext.SaveChanges();
            _toastNotification.Success("Task Updated Successfully");
            return RedirectToAction("TodoTask");
        }

        /*Toggle Status*/
        public IActionResult ToggleStatus(int Id)
        {
            var todo = TodoContext.Todos.FirstOrDefault(task => task.Id == Id);
            todo.status = todo.status == "NO"? "YES": "NO";
            if(todo.status == "NO")
            {
                _toastNotification.Success("Task uncomplete");
            }else
            {
                _toastNotification.Success("task Complete");

            }
            TodoContext.SaveChanges();
            return RedirectToAction("TodoTask");
        }

        public IActionResult CannotRemove()
        {
            return View();
        }



    }
}
