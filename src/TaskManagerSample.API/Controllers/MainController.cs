using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Core.Notifications;

namespace TaskManagerSample.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;
    public readonly IUser _appUser;

    protected Guid UserId { get; set; }
    protected bool AuthenticatedUser { get; set; }

    protected MainController(INotifier notifier,
                             IUser appUser)
    {
        _notifier = notifier;
        _appUser = appUser;

        if (_appUser.IsAuthenticated())
        {
            UserId = appUser.GetUserId();
            AuthenticatedUser = true;
        }
    }

    protected bool ValidOperation()
    {
        return !_notifier.HasNotification();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifiyErrorModelInvalid(modelState);
        return CustomResponse();
    }

    protected void NotifiyErrorModelInvalid(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(errorMsg);
        }
    }

    protected void NotifyError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
}