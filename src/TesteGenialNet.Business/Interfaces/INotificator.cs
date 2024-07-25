using FluentValidation.Results;
using System.Collections.Generic;
using TesteGenialNet.Business.Models;

namespace TesteGenialNet.Business.Interfaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void NoticationErrors(string message);
        void NotificationValidationResult(ValidationResult validationResult);
    }
}
