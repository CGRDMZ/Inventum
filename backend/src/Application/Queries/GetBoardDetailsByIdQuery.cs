using Application.Models;
using MediatR;

namespace Application.Queries
{
    public class GetBoardDetailsByIdQuery : IRequest<BoardDto>
    {
        public string BoardId { get; init; }
        public string UserId { get; init; }


    }
}