using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("card")]
public class CardController : ControllerBase
{
    private IMediator _mediator;

    public CardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{cardId}")]
    public async Task<IActionResult> GetCardDetail(string cardId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var res = await _mediator.Send(new GetCardDetailsByIdQuery
        {
            UserId = userId,
            CardId = cardId
        });

        return Ok(res);
    }

    [HttpPost("{cardId}/addChecklist")]
    public async Task<IActionResult> AddCheckListComponent(string cardId, AddCheckListComponentRequest req) {

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var res = await _mediator.Send(new AddCheckListComponentToCardCommand
        {
            UserId = userId,
            CardId = cardId,
            Name = req.Name
        });

        return Ok(res);
    }

    [HttpPost("{cardId}/checklist/{checkListId}/addItem")]
    public async Task<IActionResult> AddCheckListItem(string cardId, string checkListId, AddCheckListItemRequest req) {

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var res = await _mediator.Send(new AddCheckListItemCommand
        {
            UserId = userId,
            CardId = Guid.Parse(cardId),
            CheckListId = Guid.Parse(checkListId),
            Content = req.Content
        });

        if (!res.Success)
        {
            return BadRequest(res);
        }

        return Ok(res);
    }
}