using System;
using MediatR;

namespace Application.Commands
{
    public class SwapCardsCommand : IRequest<ResultWrapper<Unit>>
    {
        public string UserId { get; init; }
        public string BoardId { get; init; }
        public string CardGroupId { get; init; }
        
        public string FirstCardId { get; init; }
        
        public string SecondCardId { get; init; }
    }
}