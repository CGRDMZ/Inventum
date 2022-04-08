using System;
using MediatR;

namespace Application.Commands;

public class AddCheckListItemCommand : IRequest<ResultWrapper<Unit>>
{
    public string Content { get; set; }

    public string UserId { get; set; }

    public Guid CardId { get; set; }
    public Guid CheckListId { get; set; }

}