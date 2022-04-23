var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "This is a GET");
app.MapPost("/", () => "This is a POST");
app.MapPut("/", () => "This is a PUT");
app.MapDelete("/", () => "This is a DELETE");

// Lambda expression
var lamdaHandler = () => "This is a lambda variable";
app.MapGet("/lamda", lamdaHandler);

// Local function
string LocalFunction() => "This is local function";
app.MapGet("/local-function", LocalFunction);

// Instance method
var instanceHandler = new InstanceHandler();
app.MapGet("/instance", instanceHandler.Hello);

// Static method
app.MapGet("/static", StaticHandler.Hello);

// Route Parameters
app.MapGet("/users/{userId}/books/{bookId}",
    (int userId, int bookId) => $"The user id is {userId} and book id is {bookId}");

app.Run();

class InstanceHandler
{
    public string Hello()
    {
        return "Hello Instance method";
    }
}

class StaticHandler
{
    public static string Hello()
    {
        return "Hello static method";
    }
}