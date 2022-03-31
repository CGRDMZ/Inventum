using MediatR;

namespace Application.Commands
{
    public class RepositionTheCardsCommand : IRequest<ResultWrapper<Unit>>
    {
        public string UserId { get; init; }

        public string BoardId { get; init; }

        public string CardGroupId { get; init; }

        public string CardIds { get; init; }


    }
}