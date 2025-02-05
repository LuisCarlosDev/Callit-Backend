using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Callit.Application.UseCases.Users;
public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";

    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Password is required");
            return false;
        }

        if (password.Length < 6)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "Your password must contain at least 6 characters");
            return false;
        }

        return true;
    }
}