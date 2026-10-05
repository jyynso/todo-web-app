namespace TodoAPI.Model
{
	public class TodoItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateOnly DueDate { get; set; }
		public bool IsCompleted { get; set; }
		public bool IsDeleted { get; set; }

	}
}
