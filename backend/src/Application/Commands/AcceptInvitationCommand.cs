using MediatR;

namespace Application.Commands
{
    public class AcceptInvitationCommand : IRequest<ResultWrapper<Unit>>
    {
        public string UserId { get; init; }

        public string InvitationId { get; init; }

        public bool Accepted { get; init; }



    }
}
