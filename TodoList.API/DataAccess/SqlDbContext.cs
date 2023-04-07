using Microsoft.EntityFrameworkCore;
using TodoList.API.DataModels;

namespace TodoList.API.DataAccess;

public class SqlDbContext : DbContext
{
	public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
	{
	}

	public DbSet<TodoItemModel> TodoItems { get; set; }
}
