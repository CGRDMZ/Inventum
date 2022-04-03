using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.CardComponents;
using MediatR;

namespace Application.Commands;

public class AddCheckListComponentToCardCommandHandler : IRequestHandler<AddCheckListComponentToCardCommand, ResultWrapper<Unit>>
{
    private readonly ICardRepository _cardRepository;

    public AddCheckListComponentToCardCommandHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public async Task<ResultWrapper<Unit>> Handle(AddCheckListComponentToCardCommand request, CancellationToken cancellationToken)
    {

        var result = new ResultWrapper<Unit>()
        {
            Data = Unit.Value
        };

        var card = await _cardRepository.FindByIdAsync(Guid.Parse(request.CardId));

        if (card == null)
        {
            return result.AddError($"No card with this id: {request.CardId}");
        }

        var checkListComponent = CheckListComponent.Create(request.Name);

        card.AddNewCheckListComponent(checkListComponent);

        await _cardRepository.UpdateAsync(card);

        return result;

    }
}