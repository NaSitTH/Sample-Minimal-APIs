using Microsoft.EntityFrameworkCore;
using UseEFCore.Data;
using UseEFCore.Models;

var builder = WebApplication.CreateBuilder(args);

var conectrionString = builder.Configuration.GetConnectionString("AppDb");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(conectrionString));

var app = builder.Build();

async Task<List<Person>> GetAllPerson(AppDbContext db) => await db.Person.ToListAsync();

app.MapGet("/person", async (AppDbContext db) =>
    await db.Person.ToListAsync());

app.MapGet("/person/{id}", async (AppDbContext db, int id) =>

    await db.Person.FindAsync(id) is Person person ?
    Results.Ok(person) :
    Results.NotFound(new { errors = "No person found." }));

app.MapPost("/person", async (AppDbContext db, Person person) =>
{
    db.Person.Add(person);
    await db.SaveChangesAsync();
    return Results.Ok(await GetAllPerson(db));
});

app.MapPut("/person/{id}", async (AppDbContext db, Person person, int id) =>
{
    var result = await db.Person.FindAsync(id);

    if (result == null) return Results.NotFound(new { errors = "No person found." });

    result.Name = person.Name;
    await db.SaveChangesAsync();

    return Results.Ok(await GetAllPerson(db));
});

app.MapDelete("/person/{id}", async (AppDbContext db, int id) =>
{
    var result = await db.Person.FindAsync(id);

    if (result == null) return Results.NotFound(new { errors = "No person found." });

    db.Person.Remove(result);
    await db.SaveChangesAsync();

    return Results.Ok(await GetAllPerson(db));
});

app.Run();
