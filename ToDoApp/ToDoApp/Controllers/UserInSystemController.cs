using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Context;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class UserInSystemController : Controller
    {
        private readonly INotyfService _toastNotification;
        public UserInSystemController(INotyfService toastNotification)
        {
            _toastNotification = toastNotification;
        }


        TodoContext TodoContext = new TodoContext();
        [Authorize(Policy = "admin")]
        public IActionResult Index()
        {
            var user = TodoContext.Users.ToList();
            return View(user);
        }
        /*Update User Action*/
        public IActionResult UpdateUser(int Id)
        {
            var User = TodoContext.Users.FirstOrDefault(x => x.Id == Id);
            if (User == null)
            {
                return View("CannotRemove", "Todo");
            }
            UpdateUser newUser = new UpdateUser();
            newUser.Id = Id;
            newUser.Name = User.Name;
            newUser.Email = User.Email;
            newUser.Role = User.Role;

            return View(newUser);

        }
        public IActionResult SaveUpdateUser(int Id, UpdateUser userUpdate )
        {
            var User = TodoContext.Users.FirstOrDefault(usr => usr.Id == Id);

            User.Name = userUpdate.Name;
            User.Email = userUpdate.Email;
            User.Role = userUpdate.Role;
            TodoContext.SaveChanges();
            return RedirectToAction("Index");
        }

        /*Delete User Action*/
        public IActionResult DeleteUser(int id)
        {
            var user = TodoContext.Users.FirstOrDefault(x => x.Id == id);
            if(user == null)
            {
                return View("CannotRemove", "Todo");
            }
            var isFind1 = TodoContext.Todos.Any(x => x.UserId == id);
            var isFind2 =  TodoContext.Categories.Any(x => x.UserId == id) ;
            Console.WriteLine(isFind2);
            Console.WriteLine(isFind1);
            if (isFind1 != false || isFind2 != false)
            {
                _toastNotification.Error("Cannot Remove this User, he is assign to category or Tasks");
            }else
            {
                _toastNotification.Success("User is Removed");
                TodoContext.Users.Remove(user);
                TodoContext.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}
