using Application.Domain;
using FluentValidation;

namespace Application.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 16);
        RuleFor(x => x.ConnectionId).NotEmpty();
    }
}
