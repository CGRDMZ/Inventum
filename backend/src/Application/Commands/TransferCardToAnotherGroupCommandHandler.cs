using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Commands
{
    public class TransferCardToAnotherGroupCommandHandler : IRequestHandler<TransferCardToAnotherGroupCommand, ResultWrapper<Unit>>
    {
        private readonly IBoardRepository _boardRepository;

        public TransferCardToAnotherGroupCommandHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<ResultWrapper<Unit>> Handle(TransferCardToAnotherGroupCommand req, CancellationToken cancellationToken)
        {
            var result = new ResultWrapper<Unit>()
            {
                Data = Unit.Value
            };

            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));
            if (!board.IsAccessiableBy(Guid.Parse(req.UserId)))
            {
                result.AddError("This user cannot modify this board.");
                return result;
            }

            var cardGroup = board.CardGroups.SingleOrDefault(cg => cg.CardGroupId == Guid.Parse(req.CardGroupId));
            if (cardGroup == null)
            {
                result.AddError($"There is no existing card group with this id: {req.CardGroupId}");
            }
            var targetCardGroup = board.CardGroups.SingleOrDefault(cg => cg.CardGroupId == Guid.Parse(req.TargetCardGroupId));
            if (targetCardGroup == null)
            {
                result.AddError($"There is no existing card group with this id: {req.CardGroupId}");
                return result;
            }

            var card = cardGroup.Cards.SingleOrDefault(c => c.CardId == Guid.Parse(req.CardId));
            if (card == null)
            {
                result.AddError($"Card with id {req.CardId} does not exist.");
                return result;
            }

            card.TransferTo(targetCardGroup);

            // Adding te activity        
            var user = board.OwnerWithId(Guid.Parse(req.UserId));
            var activity = Activity.New(user,
                     $"Card with the content \"{card.Content}\" was transfered from group \"{cardGroup.Name}\" " +
                    $"to group \"{targetCardGroup.Name}\".", board);
            board.AddActivity(activity);

            await _boardRepository.UpdateAsync(board);

            return result;
        }
    }
}