using System;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class OpenNewBoardCommand : ICommand, IRequest<Guid>
    {

        public string UserId { get; init; }
        public string Name { get; init; }


    }
}
