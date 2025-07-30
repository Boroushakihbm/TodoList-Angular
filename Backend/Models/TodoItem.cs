namespace TodoApi.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty; 
    public DateTime Date { get; set; } 
}


