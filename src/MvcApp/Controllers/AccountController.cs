using System.Threading.Tasks;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcApp.Models;
using Infrastructure.Identity;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Controllers
{
    public class AccountController : Controller
    {
        private ILogger<AccountController> _logger;
        private readonly IMediator _mediator;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(ILogger<AccountController> logger, IMediator mediator, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _mediator = mediator;
            _signInManager = signInManager;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var model = new ProfileModel()
            {
                Username = username,
                Email = email
            };


            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateNewUserRequestModel req)
        {
            if (!ModelState.IsValid)
            {
                return View(req);
            }

            var command = new CreateNewUserCommand()
            {
                Username = req.Username,
                Email = req.Email,
                Password = req.Password
            };

            _logger.LogInformation(req.Username);
            var result = await _mediator.Send(command);


            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel req, [FromQuery] string returnUrl = null)
        {
            _logger.LogInformation(req.Username);
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("User could not be logged in.");
                return View(req);
            }
            returnUrl = returnUrl ?? "~/";
            var result = await _signInManager.PasswordSignInAsync(req.Username, req.Password, false, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User successfully logged in.");
                return LocalRedirect(returnUrl);
            }
            return View();
        }
    }

}