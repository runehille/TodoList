using TodoList.API.Models;

namespace TodoList.API.Repositories.Interfaces
{
    public interface ITodoItemsRepository
    {
        Task<List<TodoItem>> GetAllTodoItemsAsync();
        Task<TodoItem> GetTodoItemByIdAsync(string? id);
        Task<TodoItem> CreateNewTodoItemAsync(TodoItem item);
        Task<TodoItem> EditTodoItemAsync(TodoItem item);
        Task DeleteTodoItemAsync(string? id);
    }
}