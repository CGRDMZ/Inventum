using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.Models;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("board")]
    public class BoardController : ControllerBase
    {
        private IMediator _mediator;

        public BoardController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<BoardDetailsDto>), 200)]
        public async Task<IActionResult> FindBoards()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new GetBoardsOfUserByIdQuery
            {
                UserId = userId
            });

            return Ok(res);
        }

        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetBoardDetail(string boardId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new GetBoardDetailsByIdQuery
            {
                UserId = userId,
                BoardId = boardId
            });

            return Ok(res);
        }

        [HttpGet("{boardId}/activities")]
        public async Task<IActionResult> GetBoardActivities(string boardId, int page = 1, int limit = 10)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new GetBoardActivitiesByIdQuery
            {
                UserId = userId,
                BoardId = boardId,
                Page = page,
                Limit = limit,
            });

            return Ok(res);
        }

        [HttpPost("createBoard")]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new OpenNewBoardCommand
            {
                UserId = userId,
                Name = req.Name,
                BgColor = req.Color
            });

            return Ok(res);
        }

        [HttpDelete("{boardId}")]
        [ProducesResponseType(typeof(ResultWrapper<Unit>), 200)]
        [ProducesResponseType(typeof(ResultWrapper<Unit>), 400)]
        public async Task<IActionResult> DeleteBoard(string boardId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new RemoveBoardCommand
            {
                UserId = userId,
                BoardId = boardId
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserToBoardRequest req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new InviteUserToBoardCommand
            {
                InvitedBy = userId,
                BoardId = req.BoardId,
                InvitedUserUsername = req.InvitedUserUsername
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("handleInvitation")]
        public async Task<IActionResult> HandleInvitation([FromBody] HandleInvitationRequest req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new AcceptInvitationCommand
            {
                UserId = userId,
                InvitationId = req.InvitationId,
                Accepted = req.Accepted
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("{boardId}/addCardGroup")]
        public async Task<IActionResult> AddCardGroup(string boardId, [FromBody] AddCardGroupRequest req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new AddNewCardGroupToBoardCommand
            {
                OwnerUserId = userId,
                BoardId = boardId,
                CardGroupName = req.CardGroupName
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("{boardId}/cardGroup/{cardGroupId}/addCard")]
        public async Task<IActionResult> AddCard(string boardId, string cardGroupId, [FromBody] AddCardRequest req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new AddNewCardToCardGroupCommand
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = cardGroupId,
                Content = req.Content,
                BgColor = req.BgColor
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("{boardId}/cardGroup/{cardGroupId}/repositionCards")]
        public async Task<IActionResult> RepositionCards(string boardId, string cardGroupId, [FromQuery] string cardIds)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new RepositionTheCardsCommand
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = cardGroupId,
                CardIds = cardIds
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("{boardId}/transferCard/{cardId}")]
        public async Task<IActionResult> TransferCard(string boardId, string fromCardGroup, string toCardGroup, string cardId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new TransferCardToAnotherGroupCommand
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = fromCardGroup,
                TargetCardGroupId = toCardGroup,
                CardId = cardId
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpDelete("{boardId}/cardGroup/{cardGroupId}/card/{cardId}")]
        public async Task<IActionResult> removeCard(string boardId, string cardGroupId, string cardId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("You better login.");
            }

            var res = await _mediator.Send(new RemoveCardCommand
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = cardGroupId,
                CardId = cardId
            });

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

    }
}