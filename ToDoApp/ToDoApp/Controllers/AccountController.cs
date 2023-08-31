using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApp.Context;
using ToDoApp.Models;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class AccountController : Controller
    {
        TodoContext TodoContext = new TodoContext();
        public IActionResult Index()
        {
            return View();
        }

        /*Login Action*/
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> SaveLogin(LoginCred newLogin)
        {

            if(ModelState.IsValid)
            {
                var user = TodoContext.Users.FirstOrDefault(user => (user.Email == newLogin.Email && user.Password == newLogin.Password));
                if(user== null)
                {
                    ViewBag.ErrorMessage = "uncorrect Email or Password";
                    return View("Login", newLogin);
                }
                /*First Step*/
                var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                claimIdentity.AddClaim(new Claim(ClaimTypes.Sid, $"{user.Id.ToString()}"));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, $"{user.Role}"));
                var x = User.Claims.FirstOrDefault(usr => usr.Type == ClaimTypes.Role);
                Console.WriteLine($"x value is : {x}");
                Console.WriteLine(ClaimTypes.Role);

                /*Second Step*/
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                /*Third Step*/
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1),
                });



                return RedirectToAction("Index", "Todo");
            }
            return View("Login", newLogin);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SaveSignIn(User newUser)
        {
            var emailFound = TodoContext.Users.Any(user => user.Email == newUser.Email);

            if (ModelState.IsValid && !emailFound)
            {
                newUser.Role = "user";
                DateTime save = DateTime.Now;
                newUser.singupTime = new DateTime(save.Year, save.Month, save.Day, save.Hour, save.Minute, (int)save.Second);
                TodoContext.Users.Add(newUser);
                TodoContext.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.ErroMessage = "Email is Currently Found";
            return View("SignIn", newUser);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Todo");
        }

    }
}
