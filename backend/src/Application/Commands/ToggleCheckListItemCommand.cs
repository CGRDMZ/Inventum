using System;
using MediatR;

namespace Application.Commands;

public class ToggleCheckListItemCommand : IRequest<ResultWrapper<Unit>> {
    public string UserId { get; set; }
    public Guid CardId { get; set; }
    public Guid CheckListId { get; set; }
    public Guid CheckListItemId { get; set; }
}