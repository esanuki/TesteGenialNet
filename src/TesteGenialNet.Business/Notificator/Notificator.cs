using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Models;

namespace TesteGenialNet.Business.Notificator
{

    public class Notificator : INotificator
    {
        private List<Notification> _notifications;

        public Notificator()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications()
            => _notifications;

        public bool HasNotification()
            => _notifications.Any();

        public void NoticationErrors(string message)
        {
            Handle(new Notification(message));
        }
        public void NotificationValidationResult(ValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            foreach (var erro in errors)
                Handle(new Notification(erro));
        }

        private void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }
    }
}
