using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Commands
{
    public class AddNewCardGroupToBoardHandler : IRequestHandler<AddNewCardGroupToBoardCommand>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IUserRepository _userRepository;

        public AddNewCardGroupToBoardHandler(IBoardRepository boardRepository, IUserRepository userRepository)
        {
            _boardRepository = boardRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddNewCardGroupToBoardCommand command, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.FindByIdAsync(Guid.Parse(command.BoardId));
            var user = await _userRepository.FindByIdAsync(Guid.Parse(command.OwnerUserId));

            if (board.Owner.UserId != user.UserId) {
                throw new Exception("Only owner can make updates to a board.");
            }

            board.AddNewCardGroup(command.BoardName?? null);

            await _boardRepository.UpdateAsync(board);

            return Unit.Value;
        }
    }
}