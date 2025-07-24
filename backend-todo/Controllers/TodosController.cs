using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_todo.Data;
using backend_todo.Models;

namespace backend_todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetTodos()
        {
            return await _context.Todos
                .Select(todo => TodoToDTO(todo))
                .ToListAsync();

        }

        // GET: api/Todos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDTO>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return TodoToDTO(todo);
        }

        // PUT: api/Todos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, TodoDTO todoDTO)
        {
            if (id != todoDTO.Id)
            {
                return BadRequest();
            }

            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = todoDTO.Name;
            todo.IsComplete = todoDTO.IsComplete;
            todo.CategoryId = todoDTO.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Todos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(TodoDTO todoDTO)
        {
            var todoItem = new Todo
            {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name,
                CategoryId = todoDTO.CategoryId
            };

            _context.Todos.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodo),
                new { id = todoItem.TodoId },
                TodoToDTO(todoItem));

        }

        // DELETE: api/Todos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.TodoId == id);
        }

        private static TodoDTO TodoToDTO(Todo todo) =>
            new TodoDTO
            {
                Id = todo.TodoId,
                Name = todo.Name,
                IsComplete = todo.IsComplete,
                CategoryId = todo.CategoryId
            };

        private static Todo DTOToItem(TodoDTO todoDTO) =>
            new Todo
            {
                TodoId = todoDTO.Id,
                Name = todoDTO.Name,
                IsComplete = todoDTO.IsComplete,
                CategoryId = todoDTO.CategoryId
            };
    }


}
