namespace WebApi.Controllers;

using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public UserController(IMediator mediator, UserManager<AppUser> userManager, IJwtTokenService jwtTokenService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewUser(CreateNewUserRequest req)
    {
        var result = await _mediator.Send(new CreateNewUserCommand()
        {
            Username = req.Username,
            Email = req.Email,
            Password = req.Password
        });


        return Ok(result);
    }

    [HttpPost("getAccessToken")]
    public async Task<IActionResult> GetAccessToken(GetAccessTokenRequest req)
    {

        var user = _userManager.Users.SingleOrDefault(u => u.UserName == req.Username);

        if (user == null)
        {
            return NotFound($"User with name: {req.Username} not found.");
        }

        var valid = await _userManager.CheckPasswordAsync(user, req.Password);

        if (valid) {
            var token = _jwtTokenService.GenerateToken(user);
            return Ok(token);
        }

        return Unauthorized(user);
    }
}
