using FluentValidation;

namespace TaskManagerSample.Core.Models.Validations;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
    }
}