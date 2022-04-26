
using FluentValidation;
using FluentValidation.AspNetCore;
using UseFluentValidation.Models;
using UseFluentValidation.Validation;

var builder = WebApplication.CreateBuilder(args);

// FluentValidation.DependencyInjectionExtensions
// automatically find all the validators in a specific assembly using an extension method
// By default, these will be registered as Scoped
// but you can optionally use Singleton or Transient
//builder.services.AddValidatorsFromAssemblyContaining<UserValidator>(ServiceLifetime.Transient);

builder.Services.AddValidatorsFromAssemblyContaining<Person>();

//builder.Services.AddFluentValidation(fv =>
//    fv.RegisterValidatorsFromAssemblyContaining<Person>());

// Load using a type refernce rather than the generic.
//builder.Services.AddValidatorsFromAssemblyContaining(typeof(PersonValidator));

// Load an assembly reference rather than using a marker type.
//builder.Services.AddValidatorsFromAssembly(Assembly.Load("SomeAssembly"));

var app = builder.Build();

app.MapPost("/person", (Person? person, IValidator<Person> validator) =>
{
    if (person is null)
    {
        return Results.BadRequest(new { errors = "HTTP request body must not be empty." });
    }

    var validationResult = validator.Validate(person);

    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(x => x.ErrorMessage);

        return Results.BadRequest(errors);
    }

    // do something
    return Results.Ok(new { message = "No error" });
});

app.Run();
