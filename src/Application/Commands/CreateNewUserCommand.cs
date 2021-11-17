using System;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class CreateNewUserCommand : ICommand, IRequest<Guid>
    {
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        
    }
}