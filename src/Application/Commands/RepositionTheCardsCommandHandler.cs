using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using System.Collections.Generic;
using Application.Interfaces;

namespace Application.Commands
{
    public class RepositionTheCardsCommandHandler : IRequestHandler<RepositionTheCardsCommand, ResultWrapper<Unit>>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ICardService _cardService;

        public RepositionTheCardsCommandHandler(IBoardRepository boardRepository, ICardService cardService)
        {
            _boardRepository = boardRepository;
            _cardService = cardService;
        }

        public async Task<ResultWrapper<Unit>> Handle(RepositionTheCardsCommand req, CancellationToken cancellationToken)
        {
            var result = new ResultWrapper<Unit>() { Data = Unit.Value };

            var reqCardIds = req.CardIds.Split(',').Select(id => Guid.Parse(id)).ToHashSet().ToList();

            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));
            if (!board.IsAccessiableBy(Guid.Parse(req.UserId))) {
                result.AddError("This user cannot modify this board.");
                return result;
            }

            IEnumerable<Card> cards;
            try {
                cards = _cardService.GetCards(board, Guid.Parse(req.UserId), Guid.Parse(req.CardGroupId), reqCardIds);
            } catch (Exception e) {
                result.AddError(e.Message);
                return result;
            }

            var cardIds = cards.Select(c => c.CardId).ToHashSet();


            if (!cardIds.SetEquals(reqCardIds.ToHashSet()))
            {
                result.AddError("You should provide all of the Ids for the cards");
                return result;
            }

            // TODO: refactor this to prevent domain logic leaking to application layer.
            int pos = 0;
            reqCardIds.ForEach(id =>
            {
                var card = cards.Single(c => c.CardId == id);
                card.ChangePosition(pos++);
            });

            // Adding the activity
            var cardGroupName = board.CardGroups.Single( cg => cg.CardGroupId == Guid.Parse(req.CardGroupId)).Name;
            var user = board.OwnerWithId(Guid.Parse(req.UserId));
            var activity = Activity.New(user, $"Cards in the group named {cardGroupName} are repositioned by {user.Username}", board);
            board.AddActivity(activity);

            await _boardRepository.UpdateAsync(board);

            return result;

        }
    }
}