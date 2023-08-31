using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is Required")]
        [MinLength(2,ErrorMessage = "Min Length is 2 letter")]
        [MaxLength(30, ErrorMessage = "Max Length is 30 letter")]
        public string Name { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Todo>? Todo { get; set; }
    }
}
