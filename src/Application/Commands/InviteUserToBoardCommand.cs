using System;
using MediatR;

namespace Application.Commands
{
    public class InviteUserToBoardCommand : IRequest<ResultWrapper<Unit>>
    {
        public string BoardId { get; init; }
        
        public string InvitedUserUsername { get; init; }

        public string InvitedBy {get; init; }
        
    }
}