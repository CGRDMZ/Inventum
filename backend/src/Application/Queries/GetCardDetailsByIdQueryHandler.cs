using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;

namespace Application.Queries;

public class GetCardDetailsByIdQueryHandler : IRequestHandler<GetCardDetailsByIdQuery, CardWithComponentsDto>
{
    private readonly ICardRepository _cardRepository;

    public GetCardDetailsByIdQueryHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public async Task<CardWithComponentsDto> Handle(GetCardDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var card = await _cardRepository.FindByIdAsync(Guid.Parse(request.CardId));

        if (card == null)
        {
            throw new Exception("No card with this id: " + request.CardId);
        }

        return new CardWithComponentsDto()
        {
            CardId = card.CardId.ToString(),
            Content = card.Content,
            Description = card.Description,
            CardColor = card.Color.ToString(),
            Position = card.Position,
            CheckListComponents = card.CheckListComponents
            .Select(c =>
                new CheckListComponentDto
                {
                    CheckListComponentId = c.CheckListComponentId,
                    Name = c.Name,
                    CheckListItems = c.CheckListItems.Select(
                        i => new CheckListItemDto
                        {
                            CheckListItemId = i.CheckListItemId,
                            Content = i.Content,
                            IsChecked = i.IsChecked,
                            Position = i.Position
                        }).ToList()
                }).ToList(),
        };
    }
}