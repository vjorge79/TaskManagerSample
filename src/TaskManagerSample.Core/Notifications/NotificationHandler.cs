using FluentValidation;
using FluentValidation.Results;
using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Core.Models;

namespace TaskManagerSample.Core.Notifications;

public abstract class NotificationHandler
{
    private readonly INotifier _notifier;

    protected NotificationHandler(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    protected void Notify(string mensagem)
    {
        _notifier.Handle(new Notification(mensagem));
    }

    protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }
}