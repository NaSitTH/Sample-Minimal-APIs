// Routes can be CORS enabled using CORS policies.
// CORS can be declared via the [EnableCors] attribute 
// or by using the RequireCors method.

using Microsoft.AspNetCore.Cors;

const string AllowSpecificOrigins = "_allowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://example.com");
                      });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/cors1", [EnableCors(AllowSpecificOrigins)] () =>
                           "This endpoint allows cross origin requests!");

app.MapGet("/cors2", () => "This endpoint allows cross origin requests!")
                     .RequireCors(AllowSpecificOrigins);

app.Run();