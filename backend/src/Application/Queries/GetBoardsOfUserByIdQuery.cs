using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Queries
{
    public class GetBoardsOfUserByIdQuery : IRequest<List<BoardDetailsDto>>
    {
        public string UserId { get; init; }
    }
}