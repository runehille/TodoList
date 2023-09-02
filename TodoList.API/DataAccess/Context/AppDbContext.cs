using Microsoft.EntityFrameworkCore;
using TodoList.API.Models;

namespace TodoList.API.DataAccess.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().ToContainer("TodoItems");
    }


    public DbSet<TodoItem> TodoItems { get; set; }
}
