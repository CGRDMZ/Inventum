using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;

namespace Application.Commands
{
    public class AddNewCardGroupToBoardCommandHandler : IRequestHandler<AddNewCardGroupToBoardCommand, ResultWrapper<CardGroupDto>>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IUserRepository _userRepository;

        public AddNewCardGroupToBoardCommandHandler(IBoardRepository boardRepository, IUserRepository userRepository)
        {
            _boardRepository = boardRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultWrapper<CardGroupDto>> Handle(AddNewCardGroupToBoardCommand command, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.FindByIdAsync(Guid.Parse(command.BoardId));

            var result = new ResultWrapper<CardGroupDto>()
            {
                Errors = new List<string>()
            };

            if (board.Owner.UserId != Guid.Parse(command.OwnerUserId))
            {
                result.Errors.Add("Only owner can make updates to a board.");
            }

            if (!result.Success) {
                return result;
            }

            board.AddNewCardGroup(command.CardGroupName ?? null);

            var activity = Activity.New(board.Owner, $"New card group was added by {board.Owner.Username}", board);
            board.AddActivity(activity);

            await _boardRepository.UpdateAsync(board);

            return result;
        }
    }
}