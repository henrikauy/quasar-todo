namespace backend_todo.Models
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsComplete { get; set; }
        public int CategoryId { get; set; }
    }
}
