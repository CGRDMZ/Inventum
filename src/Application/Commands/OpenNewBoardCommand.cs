using System;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class OpenNewBoardCommand : ICommand, IRequest<Guid>
    {

        public string UserId { get; }
        public string Name { get; }

        public OpenNewBoardCommand(string userId, string name, string backgroundColorRGB)
        {
            UserId = userId;
        }

    }
}
