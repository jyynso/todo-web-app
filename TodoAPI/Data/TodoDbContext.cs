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
			modelBuilder.Entity<TodoItem>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
				entity.Property(e => e.Description).HasMaxLength(500);
				entity.Property(e => e.DueDate).IsRequired();
				entity.Property(e => e.IsCompleted).IsRequired();

				entity.ToTable(t => t.HasCheckConstraint(
					"DueDateCannotBeInThePast",
					"[DueDate] >= CAST(GETDATE() AS DATE)"));
			});
		}
	}
}
