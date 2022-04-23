var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/swag", () => "Hello Swagger!");

app.MapGet("/swag/{id}", (int id) => $"id : {id}");

app.MapGet("/skip", () => "Skipping Swagger.")
                    .ExcludeFromDescription();

// uses the built-in result types to customize the response
app.MapGet("/customer/{id}", (int id) =>
    id == 1
    ? Results.Ok(new Customer { Id = 1, Name = "John" })
    : Results.NotFound())
    .Produces<Customer>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.Run();

class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
}