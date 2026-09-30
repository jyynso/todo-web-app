using TodoAPI.Model;

namespace TodoAPI.Services
{
	public class TodoService : ITodoService
	{
		private readonly List<TodoItem> todoItems = new();
		private int nextId = 1;
		public Task<List<TodoItem>> GetTodoItemsAsync()
		{
			return Task.FromResult(todoItems);
		}
		public Task<TodoItem> GetTodoItemAsync(int id)
		{
			return Task.FromResult(todoItems.FirstOrDefault(t => t.Id == id));
		}

		public Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
		{
			todoItem.Id = nextId++;
			todoItems.Add(todoItem);
			return Task.FromResult(todoItem);
		}

		public Task<bool> DeleteTodoItemAsync(int id)
		{
			return Task.FromResult(todoItems.RemoveAll(t => t.Id == id) > 0);
		}

		public Task<TodoItem> UpdateTodoItemAsync(int id, TodoItem todoItem)
		{
			var existingItem = todoItems.FirstOrDefault(t => t.Id == id);
			if (existingItem != null)
			{
				existingItem.Title = todoItem.Title;
				existingItem.Description = todoItem.Description;
				existingItem.DueDate = todoItem.DueDate;
				existingItem.IsCompleted = todoItem.IsCompleted;
			}
			return Task.FromResult(existingItem);
		}
	}
}
