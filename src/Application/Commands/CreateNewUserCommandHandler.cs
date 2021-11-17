using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class CreateNewUserCommandHandler : IRequestHandler<CreateNewUserCommand, Guid>
    {
        IUserService _userService;
        public CreateNewUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Guid> Handle(CreateNewUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.CreateNewUser(command);

        }
    }
}