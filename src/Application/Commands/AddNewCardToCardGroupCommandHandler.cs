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

            var cardGroup = board.CardGroups.FirstOrDefault(cg => cg.CardGroupId == Guid.Parse(req.CardGroupId));

            if (board == null || cardGroup == null)
            {
                var errors = new List<string>();
                if (board == null) errors.Add($"There is no board with this Id: {req.BoardId}");
                if (cardGroup == null) errors.Add($"There is no card group with this id: {req.CardGroupId}");

                return new ResultWrapper<CardDto>()
                {
                    Errors = errors
                };
            }
            var newCard = Card.CreateNew(req.Content, Color.FromHexCode(req.BgColor), cardGroup);

            cardGroup.AddNewCard(newCard);

            // Adding the activity
            var activity = Activity.New(board.Owner, $"New car is added by {board.Owner.Username}", board);
            board.AddActivity(activity);

            await _boardRepository.UpdateAsync(board);


            return new ResultWrapper<CardDto>() {
                Data = new CardDto() {
                    CardId = newCard.CardId.ToString(),
                    Content = newCard.Content,
                    CardColor = newCard.Color.ToString(),
                    Position = newCard.Position,
                },
                Errors = new List<string> {}
            };


        }
    }
}