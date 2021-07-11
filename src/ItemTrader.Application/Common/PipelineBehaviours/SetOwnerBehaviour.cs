using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Interfaces;
using MediatR;

namespace ItemTrader.Application.Common.PipelineBehaviours
{
    public class SetOwnerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public SetOwnerBehaviour(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IHasOwner ownedRequest && string.IsNullOrWhiteSpace(ownedRequest.OwnerId))
            {
                var currentUserId = _currentUserService.UserId;
                ownedRequest.OwnerId = currentUserId;
            }

            return await next();
        }
    }
}
