using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage  = "Name is Required")]
        [MinLength(3, ErrorMessage = "min Length is 3 letters")]
        [MaxLength(20, ErrorMessage = "Max Length is 20 letters")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(8, ErrorMessage = "Min Length is 8 letter")]
        [MaxLength(20, ErrorMessage = "Max Length is 20 letter")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm Password is Required")]
        [Compare("Password", ErrorMessage = "Confrim Password didn't match Password")]
        public string confirmPassword { get; set; }
        /* didn't appear to user to Enter it.*/
        public DateTime singupTime { get; set; }
        public string? Role { get; set; }
        /*Relation Between Table*/
        public ICollection<Todo>? Todo { get; set; }
        public ICollection<Category>? Category { get; set; }


    }
}
