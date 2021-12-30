using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Commands
{
    public class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, ResultWrapper<Unit>>
    {
        private readonly IUserRepository _userRepository;

        public AcceptInvitationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultWrapper<Unit>> Handle(AcceptInvitationCommand req, CancellationToken cancellationToken)
        {
            var result = new ResultWrapper<Unit>() {
                Data = Unit.Value
            };

            var user = await _userRepository.FindByIdAsync(Guid.Parse(req.UserId));

            if (user == null) {
                return result.AddError($"No user with this id: {req.UserId}");
            }

            var invitation = user.Invitations.Single(i => i.InvitationId == Guid.Parse(req.InvitationId));

            if (invitation == null) {
                return result.AddError($"No invitation with this id: {req.InvitationId}");
            }

            if (invitation.InvitedUser.UserId != Guid.Parse(req.UserId)) {
                return result.AddError($"This user with id: {req.UserId} is not allowed to invite others to this board.");
            }


            invitation.Handle(req.Accepted ? InvitationResult.ACCEPT : InvitationResult.REJECT);

            await _userRepository.UpdateAsync(user);

            return result;
        }
    }
}