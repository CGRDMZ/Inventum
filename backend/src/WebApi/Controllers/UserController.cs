namespace WebApi.Controllers;

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

[Route("api/user")]
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

    [HttpGet("invitations")]
    public async Task<IActionResult> GetInvitations()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null) {
            return Unauthorized("You better login.");
        }

        var res = await _mediator.Send(new InvitationsByUserIdQuery
        {
            UserId = userId
        });

        return Ok(res);
    }

    [HttpPost("invitations/{invitationId}/handle")]
    public async Task<IActionResult> HandleInvitation(string invitationId, bool accept)
    {

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null) {
            return Unauthorized("You better login.");
        }

        var res = await _mediator.Send(new AcceptInvitationCommand
        {
            InvitationId = invitationId,
            UserId = userId,
            Accepted = accept
        });

        if (!res.Success) {
            return BadRequest(res);
        }

        return Ok(res);
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
