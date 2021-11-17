namespace WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Commands;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateNewUserRequest req)
        {
            Guid result = await _mediator.Send(new CreateNewUserCommand()
            {
                Username = req.Username,
                Email = req.Email,
                Password = req.Password
            });

            return Ok(result);
        }
    }
}