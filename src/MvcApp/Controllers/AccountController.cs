using System.Threading.Tasks;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class AccountController : Controller
    {
        private ILogger<AccountController> _logger;
        private readonly IMediator _mediator;

        public AccountController(ILogger<AccountController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateNewUserRequestModel req)
        {
            var command = new CreateNewUserCommand()
            {
                Username = req.Username,
                Email = req.Email,
                Password = req.Password
            };

            _logger.LogInformation(req.Username);
            var result = await _mediator.Send(command);

            return Json(new { UserId = result.ToString() });
        }
    }
}