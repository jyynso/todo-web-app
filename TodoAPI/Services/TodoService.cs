using TodoAPI.Data;
using TodoAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Services
{
	public class TodoService : ITodoService
	{
		private readonly TodoDbContext _context;

		public TodoService(TodoDbContext context)
		{
			_context = context;
		}

		public async Task<List<TodoItem>> GetTodoItemsAsync()
		{
			return await _context.TodoItems
				.Where(t => !t.IsDeleted)
				.ToListAsync();
		}

		public async Task<TodoItem> GetTodoItemAsync(int id)
		{
			return await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
		}

		public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
		{
			_context.TodoItems.Add(todoItem);
			await _context.SaveChangesAsync();
			return todoItem;
		}

		public async Task<bool> DeleteTodoItemAsync(int id)
		{		
			var todoItem = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
			if (todoItem != null)
			{
				//_context.TodoItems.Remove(todoItem);
				todoItem.IsDeleted = true;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<TodoItem?> UpdateTodoItemAsync(int id, TodoItem todoItem)
		{
			var existingItem = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
			if (existingItem != null)
			{
				existingItem.Title = todoItem.Title;
				existingItem.Description = todoItem.Description;
				existingItem.DueDate = todoItem.DueDate;
				existingItem.IsCompleted = todoItem.IsCompleted;
				await _context.SaveChangesAsync();
			}
			return existingItem;
		}
	}
}
