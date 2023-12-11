namespace TodoList.API.Dtos;

public record TodoItemDto
{
    public string? Id { get; set; }
    public string? ShortId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? AssignedTo { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public DateTime LastModifiedTimestamp { get; set; }
}

public record TodoItemCreateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Priority { get; set; }
    public string? CreatedBy { get; set; }
    public string? AssignedTo { get; set; }
}

public record TodoItemEditDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public string? LastModifiedBy { get; set; }
    public string? AssignedTo { get; set; }
    public DateTime LastModifiedTimestamp { get; set; }
}
