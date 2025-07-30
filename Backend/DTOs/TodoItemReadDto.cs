namespace TodoApi.DTOs;

public class TodoItemReadDto
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public DateTime? Date { get; set; }
}
