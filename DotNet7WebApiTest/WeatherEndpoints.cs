namespace DotNet7WebApiTest;

public static class WeatherEndpoints
{
    private static readonly string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static RouteHandlerBuilder MapWeatherEndpoints(this IEndpointRouteBuilder groupRoute) =>
        groupRoute.MapGroup("weather")
            .MapGet("/", Get)
                .WithName("GetWeatherForecast")
                .WithOpenApi();

    private static IResult Get()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateTime.Now.AddDays(index),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return Results.Ok(forecast);
    }
}
