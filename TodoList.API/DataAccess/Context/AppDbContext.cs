using Microsoft.EntityFrameworkCore;
using TodoList.API.Models;

namespace TodoList.API.DataAccess.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Issue> Issues { get; set; }
}
