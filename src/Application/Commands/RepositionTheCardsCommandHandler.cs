using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using System.Collections.Generic;

namespace Application.Commands
{
    public class RepositionTheCardsCommandHandler : IRequestHandler<RepositionTheCardsCommand, ResultWrapper<Unit>>
    {
        private readonly IBoardRepository _boardRepository;

        public RepositionTheCardsCommandHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<ResultWrapper<Unit>> Handle(RepositionTheCardsCommand req, CancellationToken cancellationToken)
        {
            var result = new ResultWrapper<Unit>() { Errors = new List<string>(), Data = Unit.Value };

            var reqCardIds = req.CardIds.Split(',').Select(id => Guid.Parse(id)).ToList();



            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));

            if (board == null)
            {
                result.Errors.Add($"There is no Board with the id: {req.BoardId}");
                return result;
            }

            if (!board.IsAccessiableBy(Guid.Parse(req.UserId)))
            {
                result.Errors.Add($"User with id: {req.UserId} is not allowed to modify this board.");
                return result;
            }

            var cardGroup = board.CardGroups.Single(cg => cg.CardGroupId == Guid.Parse(req.CardGroupId));

            var cardIds = cardGroup.Cards.Select(c => c.CardId).ToHashSet();


            var cards = cardGroup.Cards;


            if (!cardIds.SetEquals(reqCardIds.ToHashSet()))
            {
                result.Errors.Add("You should provide all of the Ids for the cards");
                return result;
            }

            int pos = 0;
            reqCardIds.ForEach(id =>
            {
                var card = cards.Single(c => c.CardId == id);
                card.ChangePosition(pos++);
            });

            await _boardRepository.UpdateAsync(board);

            return result;

        }
    }
}