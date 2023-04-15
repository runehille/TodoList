using TodoList.API.DataModels;

namespace TodoList.API.DataAccess
{
    public interface ITodoItemsRepository
    {
        Task<List<TodoItemModel>> GetAllTodoItemsAsync();
        Task<TodoItemModel> GetTodoItemByIdAsync(Guid? id);
        Task<TodoItemModel> CreateNewTodoItemAsync(TodoItemModel item);
        Task DeleteTodoItemAsync(Guid? id);
    }
}