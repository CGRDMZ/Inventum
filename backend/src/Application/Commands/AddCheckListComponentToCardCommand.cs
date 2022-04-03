using MediatR;

namespace Application.Commands;

public class AddCheckListComponentToCardCommand : IRequest<ResultWrapper<Unit>>
{
    public string CardId { get; init; }
    public string UserId { get; init; }
    public string Name { get; init; }
    
}