using TodoAPI.Model;

namespace TodoAPI.Services
{
	public interface ITodoService
	{
			Task<List<TodoItem>> GetTodoItemsAsync();
			Task<TodoItem> GetTodoItemAsync(int id);
			Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem);
			Task<TodoItem> UpdateTodoItemAsync(int id, TodoItem todoItem);
			Task<bool> DeleteTodoItemAsync(int id);
	}
}
