using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain;
using MediatR;

namespace Application.Commands
{
    public class OpenNewBoardCommandHandler : IRequestHandler<OpenNewBoardCommand, Guid>
    {
        private IBoardRepository _boardRepository;
        private IUserRepository _userRepository;

        public OpenNewBoardCommandHandler(IBoardRepository boardRepository, IUserRepository userRepository)
        {
            _boardRepository = boardRepository;
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(OpenNewBoardCommand command, CancellationToken cancellationToken)
        {

            var owner = await _userRepository.FindByIdAsync(Guid.Parse(command.UserId));

            if (owner == null)
            {
                throw new Exception("User not found.");
            }

            var board = Board.CreateEmptyBoard(owner);

            if (command.Name != null)
            {
                board.ChangeNameTo(command.Name);
            }

            if (command.BgColor != null) {
                board.ChangeColorTo(Color.FromHexCode(command.BgColor));
            }

            // Adding the activity
            var activity = Activity.New(owner, $"Board was created.", board);
            board.AddActivity(activity);

            return (await _boardRepository.AddAsync(board)).BoardId;
        }

    }
}