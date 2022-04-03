using Application.Models;
using MediatR;

namespace Application.Queries;

public class GetCardDetailsByIdQuery : IRequest<CardWithComponentsDto>
{
    public string UserId { get; set; }
    public string CardId { get; set; }
}