using TodoList.API.Dtos;

namespace TodoList.API.Models;

public static class MappingExtensions
{
    public static IssueDto ToDto(this Issue Issue)
    {
        return new IssueDto
        {
            Id = Issue.Id,
            Title = Issue.Title,
            Description = Issue.Description,
            Status = Issue.Status,
            Priority = Issue.Priority,
            Type = Issue.Type,
            CreatedBy = Issue.CreatedBy,
            LastModifiedBy = Issue.LastModifiedBy,
            AssignedTo = Issue.AssignedTo,
            CreatedTimestamp = Issue.CreatedTimestamp,
            LastModifiedTimestamp = Issue.LastModifiedTimestamp
        };
    }

    public static Issue ToEntity(this IssueCreateDto IssueCreateDto)
    {
        return new Issue
        {
            Id = Guid.NewGuid().ToString(),
            Title = IssueCreateDto.Title,
            Description = IssueCreateDto.Description,
            Status = "To Do",
            Priority = IssueCreateDto.Priority,
            Type = IssueCreateDto.Type,
            CreatedBy = IssueCreateDto.CreatedBy,
            LastModifiedBy = IssueCreateDto.CreatedBy,
            AssignedTo = IssueCreateDto.AssignedTo,
            CreatedTimestamp = DateTime.UtcNow,
            LastModifiedTimestamp = DateTime.UtcNow
        };
    }

    public static Issue ToEntity(this IssueEditDto IssueEditDto)
    {
        return new Issue
        {
            Id = IssueEditDto.Id,
            Title = IssueEditDto.Title,
            Description = IssueEditDto.Description,
            Status = IssueEditDto.Status,
            Priority = IssueEditDto.Priority,
            LastModifiedBy = IssueEditDto.LastModifiedBy,
            AssignedTo = IssueEditDto.AssignedTo,
            LastModifiedTimestamp = DateTime.UtcNow
        };
    }

}