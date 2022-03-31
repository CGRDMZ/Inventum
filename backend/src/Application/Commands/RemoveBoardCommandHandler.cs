using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Commands
{
    public class RemoveBoardCommandHandler : IRequestHandler<RemoveBoardCommand, ResultWrapper<Unit>>
    {
        private readonly IBoardRepository _boardRepository;

        public RemoveBoardCommandHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;

        }
        public async Task<ResultWrapper<Unit>> Handle(RemoveBoardCommand req, CancellationToken cancellationToken)
        {
            var result = new ResultWrapper<Unit>() { Data = Unit.Value };

            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));
            if (!board.IsAccessiableBy(Guid.Parse(req.UserId)))
            {
                result.AddError("This user cannot modify this board.");
                return result;
            }

            await _boardRepository.DeleteAsync(board.BoardId);

            return result;
        }
    }

}
