namespace DotNet7WebApiTest;

public class Todo
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public DateTime DueDate { get; set; }
}
