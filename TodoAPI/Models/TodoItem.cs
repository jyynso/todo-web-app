using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Model
{
	public class TodoItem
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		[StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
		public string Title { get; set; }

		[StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
		public string? Description { get; set; }
		public DateOnly DueDate { get; set; }
		public bool IsCompleted { get; set; } 
		public bool IsDeleted { get; set; } 

	}
}
