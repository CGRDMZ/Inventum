using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;
using System.Linq;
using System.Collections.Generic;

namespace Application.Commands
{
    public class AddNewCardToCardGroupCommandHandler : IRequestHandler<AddNewCardToCardGroupCommand, ResultWrapper<CardDto>>
    {
        private readonly IBoardRepository _boardRepository;

        public AddNewCardToCardGroupCommandHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<ResultWrapper<CardDto>> Handle(AddNewCardToCardGroupCommand req, CancellationToken cancellationToken)
        {

            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));

            if (!board.IsAccessiableBy(Guid.Parse(req.UserId))) {
                return new ResultWrapper<CardDto>()
                                .AddError("This user cannot add new card to this board.");
            }

            var cardGroup = board.CardGroups.FirstOrDefault(cg => cg.CardGroupId == Guid.Parse(req.CardGroupId));

            if (board == null || cardGroup == null)
            {
                var result = new ResultWrapper<CardDto>();
                if (board == null) result.AddError($"There is no board with this Id: {req.BoardId}");
                if (cardGroup == null) result.AddError($"There is no card group with this id: {req.CardGroupId}");

                return result;
            }
            var newCard = Card.CreateNew(req.Content, Color.FromHexCode(req.BgColor), cardGroup);

            cardGroup.AddNewCard(newCard);

            // Adding the activity
            var user = board.OwnerWithId(Guid.Parse(req.UserId));
            var activity = Activity.New(user, $"New card is added.", board);
            board.AddActivity(activity);

            await _boardRepository.UpdateAsync(board);


            return new ResultWrapper<CardDto>() {
                Data = new CardDto() {
                    CardId = newCard.CardId.ToString(),
                    Content = newCard.Content,
                    CardColor = newCard.Color.ToString(),
                    Position = newCard.Position,
                }
            };


        }
    }
}