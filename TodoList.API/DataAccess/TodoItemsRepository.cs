﻿using Microsoft.EntityFrameworkCore;
using TodoList.API.DataModels;

namespace TodoList.API.DataAccess;

public class TodoItemsRepository : ITodoItemsRepository
{
    private readonly SqlDbContext _dbContext;

    public TodoItemsRepository(SqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TodoItemModel>> GetAllTodoItemsAsync()
    {
        var result = await _dbContext.TodoItems.ToListAsync();

        return result;
    }

    public async Task<TodoItemModel> GetTodoItemByIdAsync(Guid? id)
    {
        var todoItemModel = await _dbContext.TodoItems.FirstOrDefaultAsync(m => m.Id == id);

        return todoItemModel;
    }
}
