using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Commands;

public class ToggleCheckListItemCommandHandler : IRequestHandler<ToggleCheckListItemCommand, ResultWrapper<Unit>>
{
    private readonly ICardRepository _cardRepository;

    public ToggleCheckListItemCommandHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<ResultWrapper<Unit>> Handle(ToggleCheckListItemCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultWrapper<Unit>();

        var card = await _cardRepository.FindByIdAsync(request.CardId);

        if (card == null) {
            return result.AddError($"Card with id {request.CardId} not found.");
        }

        var checkListComponent = card.CheckListComponents.FirstOrDefault(c => c.CheckListComponentId == request.CheckListId);

        if (checkListComponent == null) {
            return result.AddError($"CheckListComponent with id: {request.CheckListId} not found.");
        }

        var checkListItem = checkListComponent.CheckListItems.FirstOrDefault(c => c.CheckListItemId == request.CheckListItemId);

        if (checkListItem == null) {
            return result.AddError($"CheckListItem with id: {request.CheckListItemId} not found.");
        }

        checkListItem.ToggleChecked();

        await _cardRepository.UpdateAsync(card);


        return result;

    }
}