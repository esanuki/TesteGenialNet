using AutoMapper;
using TesteGenialNet.Business.Interfaces;

namespace TesteGenialNet.API.CommandHandlers
{
    public class BaseCommandHandler
    {
        protected readonly INotificator _notificator;
        protected readonly IMapper _mapper;

        public BaseCommandHandler(INotificator notificator, IMapper mapper)
        {
            _notificator = notificator;
            _mapper = mapper;
        }

    }
}
