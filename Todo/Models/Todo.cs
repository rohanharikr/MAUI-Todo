namespace TODOApp.Models;

public class TODO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
};