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
    public class RemoveCardCommandHandler : IRequestHandler<RemoveCardCommand, ResultWrapper<Unit>>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ICardService _cardService;

        public RemoveCardCommandHandler(IBoardRepository boardRepository, ICardService cardService)
        {
            _boardRepository = boardRepository;
            _cardService = cardService;
        }

        public async Task<ResultWrapper<Unit>> Handle(RemoveCardCommand req, CancellationToken cancellationToken)
        {
            var result = new ResultWrapper<Unit>() { Errors = new List<string>(), Data = Unit.Value };

            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));
            if (!board.IsAccessiableBy(Guid.Parse(req.UserId)))
            {
                result.Errors.Add("This user cannot modify this board.");
                return result;
            }

            var card = _cardService.GetCards(board, Guid.Parse(req.UserId), Guid.Parse(req.CardGroupId), new[] { Guid.Parse(req.CardId) }).Single();

            // Adding the activity
            var activity = Activity.New(board.Owner, $"Card with the content \"{card.Content}\" was deleted by {board.Owner.Username}", board);
            board.AddActivity(activity);
            
            card.RemoveFromGroup();

            await _boardRepository.UpdateAsync(board);

            return result;
        }
    }
}