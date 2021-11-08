using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;

namespace Application.Commands
{
    public class OpenNewBoardCommandHandler : IAsyncCommandHandler<OpenNewBoardCommand, Guid>
    {
        private IBoardRepository _boardRepository;
        private IUserRepository _userRepository;

        public OpenNewBoardCommandHandler(IBoardRepository boardRepository, IUserRepository userRepository)
        {
            _boardRepository = boardRepository;
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(OpenNewBoardCommand command)
        {

            var owner = await _userRepository.FindByIdAsync(Guid.Parse(command.UserId));

            if (owner == null)
            {
                throw new Exception("User not found.");
            }

            var board = Board.CreateEmptyBoard(owner);

            return (await _boardRepository.AddAsync(board)).BoardId;
        }

    }
}