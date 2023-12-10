using TodoList.API.Dtos;

namespace TodoList.API.Models;

public static class MappingExtensions
{
    public static TodoItemDto ToDto(this TodoItem todoItem)
    {
        return new TodoItemDto
        {
            Id = todoItem.Id,
            ShortId = todoItem.ShortId,
            Title = todoItem.Title,
            Description = todoItem.Description,
            Status = todoItem.Status,
            Priority = todoItem.Priority,
            CreatedBy = todoItem.CreatedBy,
            LastModifiedBy = todoItem.LastModifiedBy,
            AssignedTo = todoItem.AssignedTo,
            CreatedTimestamp = todoItem.CreatedTimestamp,
            LastModifiedTimestamp = todoItem.LastModifiedTimestamp
        };
    }

}