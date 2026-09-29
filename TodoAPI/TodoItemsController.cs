using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoAPI.Model;

namespace TodoAPI
{
	public class TodoItemsController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetTodoItems()
		{
			return Ok();
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
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodoItem(int id)
		{
			return Ok();
		}
	}
}
