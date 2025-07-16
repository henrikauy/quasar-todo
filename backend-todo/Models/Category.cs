namespace backend_todo.Models
{
    public class Category
    {
        public int CategoryId { get; set; } // Primary Key
        public required string Name { get; set; } // Category name (required)

        public List<Todo> Todos { get; } = new List<Todo>(); // A category can have many Todos
    }
}
