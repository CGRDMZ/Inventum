using System;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController: ControllerBase {
        private IAsyncCommandHandler<OpenNewBoardCommand, Guid> _handler;

        public BoardController(IAsyncCommandHandler<OpenNewBoardCommand, Guid> handler) {
            _handler = handler;
        }
        
        [HttpPost]
        public async Task<Guid> CreateBoard() {
            return await _handler.Handle(new OpenNewBoardCommand(Guid.NewGuid().ToString(), "first board", "#121212"));
        }
    }
}