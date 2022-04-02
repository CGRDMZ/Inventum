using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;
using System.Linq;

namespace Application.Queries
{
    public class GetBoardDetailsByIdQueryHandler : IRequestHandler<GetBoardDetailsByIdQuery, BoardDto>
    {
        private readonly IBoardRepository _boardRepository;

        public GetBoardDetailsByIdQueryHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<BoardDto> Handle(GetBoardDetailsByIdQuery req, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.FindByIdAsync(Guid.Parse(req.BoardId));

            if (!board.IsAccessiableBy(Guid.Parse(req.UserId)))
            {
                throw new Exception("You are not authorized to view this board.");
            }

            return new BoardDto()
            {
                BoardInfo = new BoardDetailsDto()
                {
                    BoardId = board.BoardId.ToString(),
                    Name = board.Name,
                    BgColor = board.BgColor.ToString()
                },
                CardGroups = board.CardGroups.Select(b => new CardGroupDto()
                {
                    CardGroupId = b.CardGroupId.ToString(),
                    Name = b.Name,
                    Cards = b.Cards.Select(c => new CardDto()
                    {
                        CardId = c.CardId.ToString(),
                        Content = c.Content,
                        CardColor = c.Color.ToString(),
                        Position = c.Position
                    }).OrderBy(c => c.Position).ToList()
                }).ToList(),
                Activities = board.Activities.OrderByDescending(a => a.OccuredOn).Take(10).Select(a => new ActivityDto()
                {
                    OccuredOn = a.OccuredOn,
                    DoneByUser = a.DoneBy.Username,
                    Message = a.Message
                }).ToList()
            };
        }
    }
}