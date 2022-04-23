using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// get-by-id/1 
app.MapGet("/get-by-id/{id}", (int id) => $"Requesting id {id}");

// get-page?pageNumber=1
app.MapGet("/get-page", (int page) => $"Requesting page {page}");

// custom header
app.MapGet("/custom-header",
    ([FromHeader(Name = "X-CUSTOM-HEADER")] string customHeader) =>
    $"Custom-Header {customHeader}");

// model binding
app.MapPost("/book", (Book book) => book);

// read request data directly from the request
app.MapGet("/request/{id}", (HttpRequest request) =>
{
    var id = request.RouteValues["id"];
    var page = request.Query["page"];
    var customHeader = request.Headers["X-CUSTOM-HEADER"];
});

app.MapPost("/book2", async (HttpRequest request) =>
{
    var book = await request.ReadFromJsonAsync<Book>();
    return book;
});

app.Run();

public class Book
{
    public int Id { get; set; }
    public string? Name { get; set; }
}