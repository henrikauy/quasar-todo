
using Microsoft.EntityFrameworkCore;
using backend_todo.Models;

namespace backend_todo.Data
{
    public class TodoContext : DbContext
    {
        // Constructor
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        // Map properties to database tables
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>();
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Work" },
                new Category { CategoryId = 2, Name = "Chores" },
                new Category { CategoryId = 3, Name = "Personal" },
                new Category { CategoryId = 4, Name = "Misc" }
            );
        }

    }

}
