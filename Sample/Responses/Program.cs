var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// return string
app.MapGet("/string", () => "Hello World!");

// return text
app.MapGet("/text", () => Results.Text("Some text"));

// return json
app.MapGet("/json", () => new { Message = "Hello!" });

// return IResult
app.MapGet("/result", () => Results.Ok(new { Message = "Hello!" }));

// custom status code
app.MapGet("/405", () => Results.StatusCode(405));

// redirect
app.MapGet("/old-path", () => Results.Redirect("/string"));

app.Run();
