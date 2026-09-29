using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoAPI.Model;
using TodoAPI.Services;

namespace TodoAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodoItemsController : ControllerBase
	{
		private readonly ITodoService _service;
		public TodoItemsController(ITodoService service)
		{	
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetTodoItems()
		{
			var items = await _service.GetTodoItemsAsync();
			return Ok(items);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTodoItem(int id)
		{
			return Ok();		
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodoItem(int id, TodoItem todoItem)
		{
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> CreateTodoItem(TodoItem todoItem)
		{
			var created = await _service.CreateTodoItemAsync(todoItem);
			return CreatedAtAction(nameof(GetTodoItem), new { id = created.Id }, created);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodoItem(int id)
		{
			return Ok();
		}
	}
}
