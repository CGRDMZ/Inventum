using Application.Models;
using MediatR;

namespace Application.Commands
{
    public class AddNewCardToCardGroupCommand : IRequest<ResultWrapper<CardDto>>
    {
        public string BoardId { get; init; }

        public string UserId { get; init; }
        
        public string CardGroupId { get; init; }
        
        public string Content { get; init; }
        
        public string BgColor { get; init; }
    }
}