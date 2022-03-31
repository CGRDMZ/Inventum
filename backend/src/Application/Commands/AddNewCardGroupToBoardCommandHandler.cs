using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;
using System.Linq;

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

            var result = new ResultWrapper<CardGroupDto>();

            if (!board.IsAccessiableBy(Guid.Parse(command.OwnerUserId)))
            {
                result.AddError("Only owner can make updates to a board.");
            }

            if (!result.Success)
            {
                return result;
            }

            board.AddNewCardGroup(command.CardGroupName ?? null);

            // Adding the activity
            var user = board.Owners.Where(u => u.UserId == Guid.Parse(command.OwnerUserId)).Single();
            var activity = Activity.New(user, $"New card group was added.", board);
            board.AddActivity(activity);

            await _boardRepository.UpdateAsync(board);

            return result;
        }
    }
}