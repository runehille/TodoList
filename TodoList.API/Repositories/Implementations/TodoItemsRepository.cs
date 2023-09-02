using Microsoft.EntityFrameworkCore;
using TodoList.API.DataAccess.Context;
using TodoList.API.Models;
using TodoList.API.Repositories.Interfaces;

namespace TodoList.API.Repositories.Implementations;

public class TodoItemsRepository : ITodoItemsRepository
{
    private readonly AppDbContext _dbContext;

    public TodoItemsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TodoItem>> GetAllTodoItemsAsync()
    {
        var result = await _dbContext.TodoItems.ToListAsync();

        return result;
    }

    public async Task<TodoItem> GetTodoItemByIdAsync(string? id)
    {
        var todoItem = await _dbContext.TodoItems.FirstOrDefaultAsync(m => m.Id == id);

        return todoItem!;
    }
    public async Task<TodoItem> CreateNewTodoItemAsync(TodoItem todoItem)
    {
        var item = new TodoItem
        {
            Id = Guid.NewGuid().ToString(),
            Title = todoItem.Title,
            Description = todoItem.Description,
            CreatedTimestamp = DateTime.UtcNow,
            LastModifiedTimestamp = DateTime.UtcNow
        };

        _dbContext.TodoItems.Add(item);

        await _dbContext.SaveChangesAsync();

        return todoItem;
    }
    public async Task<TodoItem> EditTodoItemAsync(TodoItem todoItem)
    {

        _dbContext.Update(todoItem);
        await _dbContext.SaveChangesAsync();

        return todoItem;
    }

    public async Task DeleteTodoItemAsync(string? id)
    {
        var todoItem = await _dbContext.TodoItems.FindAsync(id);
        if (todoItem != null)
        {
            _dbContext.TodoItems.Remove(todoItem);
        }

        await _dbContext.SaveChangesAsync();
    }
    private bool TodoItemModelExists(string id)
    {
        return (_dbContext.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
