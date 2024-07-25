using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TesteGenialNet.Business.Interfaces;

namespace TesteGenialNet.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly INotificator _notificator;
        protected readonly IMapper _mapper;

        public MainController(INotificator notificator, IMapper mapper)
        {
            _notificator = notificator;
            _mapper = mapper;
        }

        protected ActionResult ReturnResponse(object result = null)
        {
            if (!_notificator.HasNotification())
            {
                return new OkObjectResult(new
                {
                    success = true,
                    data = result
                });
            }

            return new BadRequestObjectResult(new
            {
                success = false,
                errors = _notificator.GetNotifications().Select(n => n.Message)
            });
        }

        protected void NotificationErrors(string message)
        {
            _notificator.NoticationErrors(message);
        }

        protected bool HasNotification()
        {
            return _notificator.HasNotification();
        }
    }
}
