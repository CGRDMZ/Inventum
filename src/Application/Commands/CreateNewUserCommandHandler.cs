using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class CreateNewUserCommandHandler : IRequestHandler<CreateNewUserCommand, ResultWrapper<Guid>>
    {
        IUserService _userService;
        public CreateNewUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultWrapper<Guid>> Handle(CreateNewUserCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            return await _userService.CreateNewUser(command);

        }
    }
}