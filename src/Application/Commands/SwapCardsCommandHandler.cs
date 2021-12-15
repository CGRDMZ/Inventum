using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using System.Linq;

namespace Application.Commands
{
    public class SwapCardsCommandHandler : IRequestHandler<SwapCardsCommand, ResultWrapper<Unit>>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ICardLocationService _cardLocationService;

        public SwapCardsCommandHandler(IBoardRepository boardRepository, ICardLocationService cardLocationService)
        {
            _boardRepository = boardRepository;
            _cardLocationService = cardLocationService;
        }

        public async Task<ResultWrapper<Unit>> Handle(SwapCardsCommand req, CancellationToken cancellationToken)
        {

            var result = new ResultWrapper<Unit>();

            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));
            if (!board.IsAccessiableBy(Guid.Parse(req.UserId))) {
                result.Errors.Add("This user cannot modify this board.");
                return result;
            }

            var cardGroup = board.CardGroups.SingleOrDefault(cg => cg.CardGroupId == Guid.Parse(req.CardGroupId));
            if (cardGroup == null) {
                result.Errors.Add($"There is no existing card group with this id: {req.CardGroupId}");
                return result;
            }

            var firstCard = cardGroup.Cards.SingleOrDefault(c => c.CardId == Guid.Parse(req.FirstCardId));
            var secondCard = cardGroup.Cards.SingleOrDefault(c => c.CardId == Guid.Parse(req.SecondCardId));
            if (firstCard == null || secondCard == null) {
                result.Errors.Add($"One of the cards with id {req.FirstCardId} or {req.SecondCardId} does not exist.");
                return result;
            }

            _cardLocationService.SwapCardLocations(firstCard, secondCard);

            await _boardRepository.UpdateAsync(board);

            return result;

        }
    }
}