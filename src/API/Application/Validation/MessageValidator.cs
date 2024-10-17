using Application.Domain;
using FluentValidation;

namespace Application.Validation;

public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(100);
    }
}
