using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Context
{
    public class TodoContext:DbContext
    {
       
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MOHAMED\\SQLEXPRESS;database=Todo;trusted_Connection=true;Encrypt=false");
        }


    }
}
