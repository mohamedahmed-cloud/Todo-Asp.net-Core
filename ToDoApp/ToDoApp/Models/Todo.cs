using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage ="Min Lenght is 2 Letters")]
        [MaxLength(20, ErrorMessage = "Max Lenght is 20 Letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(3, ErrorMessage = "Min Lenght is 3 Letters")]
        [MaxLength(50, ErrorMessage = "Max Lenght is 50 Letters")]
        public string Description { get; set; }
        public string status { get; set; }
        
        /* didn't appear*/
        public DateTime CreatedDate { get; set; }

        /*Relation Between Table*/
        public int? UserId { get; set; }
        public User? User { get; set; }  

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        
    }
}
