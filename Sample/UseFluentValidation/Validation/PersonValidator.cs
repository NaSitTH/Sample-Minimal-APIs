using FluentValidation;
using UseFluentValidation.Models;

namespace UseFluentValidation.Validation;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(4).NotEqual("foo");
        // etc
    }
}