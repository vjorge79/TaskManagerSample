using FluentValidation;

namespace TaskManagerSample.Core.Models.Validations;

public class TaskValidation : AbstractValidator<Task>
{
    public TaskValidation()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(c => c.DueDate)
            .NotEqual(DateTime.MinValue).WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(c => c.Status)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        // TODO: Colocar regra para Pending, InProgress and Done para Status!!!
    }
}