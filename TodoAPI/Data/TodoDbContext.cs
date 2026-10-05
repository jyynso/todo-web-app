using Microsoft.EntityFrameworkCore;
using TodoAPI.Model;

namespace TodoAPI.Data
{
	public class TodoDbContext : DbContext
	{
		public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
		{
		}
		public DbSet<TodoItem> TodoItems { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<TodoItem>().HasQueryFilter(t => !t.IsDeleted);
		}
	}
}
