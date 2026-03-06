var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var tasks = new List<TaskItem>();

app.MapGet("/", () => "Minimal API funcionando!");

app.MapGet("/tasks", () => tasks);

app.MapPost("/tasks", (TaskItem task) =>
{
    task.Id = tasks.Count + 1;
    tasks.Add(task);
    return Results.Created($"/tasks/{task.Id}", task);
});

app.MapGet("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
        return Results.NotFound();

    return Results.Ok(task);
});

app.Run();

record TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }
}