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
    [Route("api/board")]
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

            if (!res.Success) {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}