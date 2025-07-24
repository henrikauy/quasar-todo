namespace backend_todo.Models
{
    public class Todo
    {
        public int TodoId { get; set; } // Primary Key
        public required string Name { get; set; } // Todo entry name (required)
        public bool IsComplete { get; set; } // Task/Todo complete or not bool
        public string? Secret { get; set; } // Secret field for DTO

        public int CategoryId { get; set; } // Foreign Key to category table
        public Category Category { get; set; } = null!; // A todo entry has one category
    }
}
