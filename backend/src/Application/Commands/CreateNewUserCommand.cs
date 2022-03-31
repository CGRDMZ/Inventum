using System;
using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class CreateNewUserCommand : ICommand, IRequest<ResultWrapper<Guid>>
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }

    }
}