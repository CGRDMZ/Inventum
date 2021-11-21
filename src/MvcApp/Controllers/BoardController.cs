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

namespace MvcApp.Controllers
{
    public class BoardController: Controller {
        private readonly ILogger<BoardController> _logger;
        private readonly IMediator _mediator;

        public BoardController(ILogger<BoardController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            userId = "ed758b45-2498-48c8-a2ca-664993d1a66a";
            _logger.LogInformation(userId);

            var result = await _mediator.Send(new GetBoardsOfUserByIdQuery() {
                UserId = userId,
            });

            _logger.LogInformation(result.Count.ToString());

            var vm = new BoardViewModel() {
                Boards = result.Select(b => 
                new BoardModel() {
                    BoardId = b.BoardId,
                    BoardName = b.Name,
                    BgColor = b.BgColor,
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBoard(BoardViewModel model) {

            if (!ModelState.IsValid)
            {
                return Redirect("~/Board");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            userId = "ed758b45-2498-48c8-a2ca-664993d1a66a";
            _logger.LogInformation("board color: " + model.CreateBoardModel.BgColor);
            _logger.LogInformation(ModelState.IsValid.ToString());

            var result = await _mediator.Send(new OpenNewBoardCommand() {
                UserId = userId,
                Name = model.CreateBoardModel.BoardName,
                BgColor = model.CreateBoardModel.BgColor
            });

            return Redirect("~/");


        }
    }
}