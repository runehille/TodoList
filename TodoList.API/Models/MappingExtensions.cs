using TodoList.API.Dtos;

namespace TodoList.API.Models;

public static class MappingExtensions
{
    public static TodoItemDto ToDto(this TodoItem todoItem)
    {
        return new TodoItemDto
        {
            Id = todoItem.Id,
            Title = todoItem.Title,
            Description = todoItem.Description,
            Status = todoItem.Status,
            Priority = todoItem.Priority,
            Type = todoItem.Type,
            CreatedBy = todoItem.CreatedBy,
            LastModifiedBy = todoItem.LastModifiedBy,
            AssignedTo = todoItem.AssignedTo,
            CreatedTimestamp = todoItem.CreatedTimestamp,
            LastModifiedTimestamp = todoItem.LastModifiedTimestamp
        };
    }

    public static TodoItem ToEntity(this TodoItemCreateDto todoItemCreateDto)
    {
        return new TodoItem
        {
            Id = Guid.NewGuid().ToString(),
            Title = todoItemCreateDto.Title,
            Description = todoItemCreateDto.Description,
            Status = "To Do",
            Priority = todoItemCreateDto.Priority,
            Type = todoItemCreateDto.Type,
            CreatedBy = todoItemCreateDto.CreatedBy,
            LastModifiedBy = todoItemCreateDto.CreatedBy,
            AssignedTo = todoItemCreateDto.AssignedTo,
            CreatedTimestamp = DateTime.UtcNow,
            LastModifiedTimestamp = DateTime.UtcNow
        };
    }

    public static TodoItem ToEntity(this TodoItemEditDto todoItemEditDto)
    {
        return new TodoItem
        {
            Id = todoItemEditDto.Id,
            Title = todoItemEditDto.Title,
            Description = todoItemEditDto.Description,
            Status = todoItemEditDto.Status,
            Priority = todoItemEditDto.Priority,
            LastModifiedBy = todoItemEditDto.LastModifiedBy,
            AssignedTo = todoItemEditDto.AssignedTo,
            LastModifiedTimestamp = DateTime.UtcNow
        };
    }

}