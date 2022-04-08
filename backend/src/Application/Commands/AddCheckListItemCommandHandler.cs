using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Commands;

public class AddCheckListItemCommandHandler : IRequestHandler<AddCheckListItemCommand, ResultWrapper<Unit>>
{
    private readonly ICardRepository _cardRepository;

    public AddCheckListItemCommandHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<ResultWrapper<Unit>> Handle(AddCheckListItemCommand req, CancellationToken cancellationToken)
    {

        var result = new ResultWrapper<Unit>();

        var card = await _cardRepository.FindByIdAsync(req.CardId);

        if (card == null)
        {
            result.AddError($"Card with id {req.CardId} not found.");
            return result;
        }

        var checkListComponent = card.CheckListComponents.FirstOrDefault(c => c.CheckListComponentId == req.CheckListId);

        if (checkListComponent == null)
        {
            return result.AddError($"CheckListComponent with id: {req.CheckListId} not found.");
        }

        checkListComponent.AddNewItem(req.Content);

        await _cardRepository.UpdateAsync(card);

        return result;


    }
}