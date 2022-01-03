using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Commands
{
    public class InviteUserToBoardCommandHandler : IRequestHandler<InviteUserToBoardCommand, ResultWrapper<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBoardRepository _boardRepository;

        public InviteUserToBoardCommandHandler(IUserRepository userRepository, IBoardRepository boardRepository)
        {
            _userRepository = userRepository;
            _boardRepository = boardRepository;
        }

        public async Task<ResultWrapper<Unit>> Handle(InviteUserToBoardCommand req, CancellationToken cancellationToken)
        {
            var res = new ResultWrapper<Unit>() {
                Data = Unit.Value
            };

            var user = await _userRepository.FindUserByUsername(req.InvitedUserUsername);

            if (user == null) {
                return res.AddError("There is no registered user with this user name.");
            }

            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));

            if (board == null) {
                return res.AddError($"There is no board with this id: {req.BoardId}");
            }

            try
            {
                var invitation = Invitation.New(board);
                user.ReceiveInvitation(invitation);
            }
            catch (DomainException ex)
            {
                 return res.AddError(ex.Message);
            }

            var activity = Activity.New(user, "Invited user.", board);
            board.AddActivity(activity);

            await _userRepository.UpdateAsync(user);
            
            return res;
        }
    }
}