using Callit.Communication.Requests;
using FluentValidation;

namespace Callit.Application.UseCases.Users.CreateUser;

public class CreateUserValidator : AbstractValidator<RequestUserJson>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email invalid");

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestUserJson>());
    }
}
