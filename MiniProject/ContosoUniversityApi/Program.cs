using System.Text.Json.Serialization;
using ContosoUniversity.Data;
using ContosoUniversityApi.Models;
using ContosoUniversityApi.Repositories;
using ContosoUniversityApi.Services;
using ContosoUniversityApi.Wrapper;
using Microsoft.EntityFrameworkCore;
using HttpJson = Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

var conectrionString = builder.Configuration.GetConnectionString("AppDb");

builder.Services.AddDbContext<SchoolContext>(x => x.UseSqlServer(conectrionString));

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<IRepository<Student>, StudentsRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Set the JSON serializer options
builder.Services.Configure<HttpJson.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// UriService for paggination
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/students", async (IStudentService studentsService) =>
{
    var response = await studentsService.GetAll();
    if (response.IsError)
        return Results.NotFound(response);

    return Results.Ok(response);

})
.Produces<Response<List<Student>>>(StatusCodes.Status200OK)
.WithTags("Get");

app.MapGet("/students/{id}", async (IStudentService studentsService, int id) =>
    {
        var response = await studentsService.GetById(id);
        if (response.IsError)
            return Results.NotFound(response);

        return Results.Ok(response);
    })
    .Produces<Response<Student>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags("Get");

app.MapGet("/students/{id}/details", async (IStudentService studentService, int id) =>
    {
        var response = await studentService.GetDetail(id);
        if (response.IsError)
            return Results.NotFound(response);

        return Results.Ok(response);
    })
    .Produces<Response<Student>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags("Get");

app.MapPost("/students", async (IStudentService studentService, Student student) =>
{
    if (student is null)
        return Results.BadRequest();

    await studentService.Add(student);
    return Results.Created($"/students/{student.Id}", student);

}).Produces<Student>(StatusCodes.Status201Created)
    .WithTags("Set");

app.MapPut("/students/{id}", async (IStudentService studentService, Student student, int id) =>
{
    var response = await studentService.Update(student, id);

    if (response.IsError)
        return Results.NotFound(response);

    return Results.Ok(response);

}).Produces<Response<Student>>(StatusCodes.Status200OK)
    .WithTags("Set");

app.MapDelete("/students/{id}", async (IStudentService studentService, int id) =>
{
    var response = await studentService.Remove(id);

    if (response.IsError)
        return Results.NotFound(response);

    return Results.NoContent();

}).Produces(StatusCodes.Status204NoContent)
    .WithTags("Delete");

app.MapGet("/students/pagination", async (IStudentService studentService, HttpRequest request) =>
{
    return await studentService.GetByPageFilter(request);
});

app.Run();
