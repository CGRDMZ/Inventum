using MediatR;

namespace Application.Commands
{
    public class TransferCardToAnotherGroupCommand : IRequest<ResultWrapper<Unit>>
    {
        public string UserId { get; init; }
        public string BoardId { get; init; }
        public string CardGroupId { get; init; }

        public string CardId { get; init; }
        public string TargetCardGroupId { get; init; }


    }
}