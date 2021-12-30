using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Queries
{
    public class InvitationsByUserIdQuery : IRequest<List<InvitationDto>>{

        public string UserId { get; init; }
    }
}