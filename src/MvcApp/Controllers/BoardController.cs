using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IMediator _mediator;

        public BoardController(ILogger<BoardController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _logger.LogInformation("The user Id is " + userId);

            var result = await _mediator.Send(new GetBoardsOfUserByIdQuery()
            {
                UserId = userId,
            });

            _logger.LogInformation(result.Count.ToString());

            var invitationResult = await _mediator.Send(new InvitationsByUserIdQuery()
            {
                UserId = userId,
            });

            var vm = new BoardViewModel()
            {
                Boards = result.Select(b =>
                new BoardMetadataModel()
                {
                    BoardId = b.BoardId,
                    BoardName = b.Name,
                    BgColor = b.BgColor,
                }).ToList(),
                Invitations = invitationResult
            };

            return View(vm);
        }

        public async Task<IActionResult> Detail(string id)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { BoardId = id });
            }

            _logger.LogInformation("Board id is: " + id);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var query = new GetBoardDetailsByIdQuery()
            {
                BoardId = id,
                UserId = userId
            };

            var result = await _mediator.Send(query);


            var vm = new BoardDetailViewModel()
            {
                Board = result
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> InviteUser(string boardId, BoardDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(model);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new InviteUserToBoardCommand()
            {
                InvitedUserUsername = model.InviteUserModel.Username,
                BoardId = boardId,
                InvitedBy = userId
            });

            if (result.Success)
            {
                return Redirect($"~/Board/Detail/{boardId}");
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptInvitation(BoardViewModel model, string invitationId, bool accepted)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {});
            }


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new AcceptInvitationCommand()
            {
                InvitationId = invitationId,
                UserId = userId,
                Accepted = accepted
            });

            if (result.Success) {
                return LocalRedirect("~/Board");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCardGroup(string id, BoardDetailViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return Json(model);
            }

            _logger.LogInformation("Card Group name is: " + model.CreateCardGroupModel.CardGroupName);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var boardId = id;

            var result = await _mediator.Send(new AddNewCardGroupToBoardCommand()
            {
                OwnerUserId = userId,
                BoardId = boardId,
                CardGroupName = model.CreateCardGroupModel.CardGroupName
            });

            if (result.Success)
            {
                _logger.LogInformation("New Card Group Added to Board with Id: " + id);
                return LocalRedirect($"~/Board/Detail/{boardId}");
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(string boardId, string cardGroupId, BoardDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("~/Board");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new AddNewCardToCardGroupCommand()
            {
                BoardId = boardId,
                UserId = userId,
                CardGroupId = cardGroupId,
                Content = model.CreateNewCardModel.Content,
                BgColor = model.CreateNewCardModel.BgColor
            });

            return LocalRedirect($"~/Board/Detail/{boardId}");

        }

        [HttpPost]
        public async Task<IActionResult> AddNewBoard(BoardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("~/Board");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _logger.LogInformation(ModelState.IsValid.ToString());

            var result = await _mediator.Send(new OpenNewBoardCommand()
            {
                UserId = userId,
                Name = model.CreateBoardModel.BoardName,
                BgColor = model.CreateBoardModel.BgColor
            });

            return Redirect("~/Board");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCard(string boardId, string cardGroupId, string cardId)
        {
            _logger.LogInformation($"card id: {cardId} is getting removed...");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new RemoveCardCommand()
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = cardGroupId,
                CardId = cardId,
            });

            return Redirect($"~/Board/Detail/{boardId}");
        }

        [HttpPost]
        public async Task<IActionResult> SwapCards(string boardId, string cardGroupId, string first, string second)
        {
            _logger.LogInformation($"first id: {first}, second id: {second}, board id: ${boardId}");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new SwapCardsCommand()
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = cardGroupId,
                FirstCardId = first,
                SecondCardId = second
            });

            return Redirect($"~/Board/Detail/{boardId}");
        }

        [HttpPost]
        public async Task<IActionResult> RepositionCards(string boardId, string cardGroupId, string cardIds)
        {
            _logger.LogInformation($"card ids: {cardIds} board id: ${boardId}");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new RepositionTheCardsCommand()
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = cardGroupId,
                CardIds = cardIds,
            });

            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> TransferCard(string boardId, string cardGroupId, string cardId, string targetCardGroupId)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = await _mediator.Send(new TransferCardToAnotherGroupCommand()
            {
                UserId = userId,
                BoardId = boardId,
                CardGroupId = cardGroupId,
                CardId = cardId,
                TargetCardGroupId = targetCardGroupId
            });

            return Json(res);
        }
    }
}