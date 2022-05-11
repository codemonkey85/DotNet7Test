namespace DotNet7WebApiTest;

public static class TodoEndpoints
{
    private static readonly IDictionary<long, Todo> Todos = new Dictionary<long, Todo>();

    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder groupRoute)
    {
        var todosGroup = groupRoute.MapGroup("todos");

        todosGroup.MapGet("/", GetTodos).WithName(nameof(GetTodos)).WithOpenApi();
        todosGroup.MapGet("/{id:long}", GetTodo).WithName(nameof(GetTodo)).WithOpenApi();
        todosGroup.MapPost("/", CreateTodo).WithName(nameof(CreateTodo)).WithOpenApi();
        todosGroup.MapPut("/{id:long}", UpdateTodo).WithName(nameof(UpdateTodo)).WithOpenApi();
        todosGroup.MapDelete("/{id:long}", DeleteTodo).WithName(nameof(DeleteTodo)).WithOpenApi();

        return todosGroup;
    }

    private static IResult GetTodos() => Results.Ok(Todos);

    private static IResult GetTodo(long id) => !Todos.TryGetValue(id, out var foundTodo) || foundTodo is null
        ? Results.NotFound()
        : Results.Ok(foundTodo);

    private static IResult CreateTodo(Todo todo)
    {
        Todos.Add(todo.Id, todo);
        return Results.Created($"/{todo.Id}", todo);
    }

    private static IResult UpdateTodo(long id, Todo todo)
    {
        if (!Todos.TryGetValue(id, out var foundTodo) || foundTodo is null)
        {
            return Results.NotFound();
        }

        var updatedTodo = new Todo
        {
            Id = id,
            Title = todo.Title,
            DueDate = todo.DueDate,
        };

        Todos.Remove(id);
        Todos.Add(id, updatedTodo);

        return Results.NoContent();
    }

    private static IResult DeleteTodo(long id)
    {
        if (!Todos.TryGetValue(id, out var foundTodo) || foundTodo is null)
        {
            return Results.NotFound();
        }

        Todos.Remove(id);

        return Results.NoContent();
    }
}
