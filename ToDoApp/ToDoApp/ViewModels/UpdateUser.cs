using System.ComponentModel.DataAnnotations;

namespace ToDoApp.ViewModels
{
    public class UpdateUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(3, ErrorMessage = "min Length is 3 letters")]
        [MaxLength(20, ErrorMessage = "Max Length is 20 letters")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
