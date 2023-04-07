namespace TodoList.Client.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public DateTime LastModifiedTimestamp { get; set; }
    public DateTime LastModifiedBy { get; set; }
}
