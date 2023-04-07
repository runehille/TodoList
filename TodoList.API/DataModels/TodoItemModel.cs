namespace TodoList.API.DataModels;

public class TodoItemModel
{
    public Guid Id { get; set; } = new Guid();
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public DateTime LastModifiedTimestamp { get; set; }
    public DateTime LastModifiedBy { get; set; }
}
