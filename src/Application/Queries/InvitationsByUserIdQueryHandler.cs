using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;

namespace Application.Queries
{
    public class InvitationsByUserIdQueryHandler : IRequestHandler<InvitationsByUserIdQuery, List<InvitationDto>>
    {

        private readonly IUserRepository _userRepository;

        public InvitationsByUserIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<InvitationDto>> Handle(InvitationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(Guid.Parse(request.UserId));
            var invitations = user.Invitations;


            return invitations.Where(i => !i.IsDeleted).Select(i => new InvitationDto()
            {
                InvitationId = i.InvitationId.ToString(),
                InvitedTo = i.InvitedTo.Name,
                InvitedUser = i.InvitedUser.Username
            }).ToList();
        }
    }
}