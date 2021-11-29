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

            var vm = new BoardViewModel()
            {
                Boards = result.Select(b =>
                new BoardMetadataModel()
                {
                    BoardId = b.BoardId,
                    BoardName = b.Name,
                    BgColor = b.BgColor,
                }).ToList()
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


            var vm = new BoardDetailViewModel() {
                Board = result
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCardGroup(string id, BoardDetailViewModel model)
        {

            if (!ModelState.IsValid) {
                return Json(model);
            }

            _logger.LogInformation("Card Group name is: " + model.CreateCardGroupModel.CardGroupName);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var boardId = id;

            var result = await _mediator.Send(new AddNewCardGroupToBoardCommand() {
                OwnerUserId = userId,
                BoardId = boardId,
                CardGroupName = model.CreateCardGroupModel.CardGroupName
            });

            if (result.Success) {
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

            var result = await _mediator.Send(new AddNewCardToCardGroupCommand() {
                BoardId = boardId,
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
    }
}