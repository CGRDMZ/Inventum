using System;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController: ControllerBase {
        private IMediator _mediator;

        public BoardController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreateBoard()
        {
            return Ok();
        }
    }
}