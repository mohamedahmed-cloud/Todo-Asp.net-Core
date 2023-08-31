using System.ComponentModel.DataAnnotations;

namespace ToDoApp.ViewModels
{
    public class LoginCred
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage = "Please Enter a valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [MinLength(8, ErrorMessage = "Min Lenght is 8 letter")]
        public string Password { get; set; }

    }
}

